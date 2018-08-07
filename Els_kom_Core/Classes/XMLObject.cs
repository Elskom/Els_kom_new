// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    /// <summary>
    /// Class in the Core that allows Reading and Writing of XML Files.
    /// </summary>
    public sealed class XMLObject: System.IDisposable
    {
        // TODO: Add functions to remove XML Entries and Attributes too.
        // TODO: Finish Read(string elementname, string attributename) and
        // Write(string elementname, string attributename, object attributevalue)
        // shortcut methods.
        // TODO: Add ways of adding, editing, and deleting elements within elements.
        private System.Xml.Linq.XDocument doc;
        private string cached_xmlfilename;
        private bool _exists = false;
        private bool _changed = false;
        // Pending XML Element Addictions (excluding only adding attributes to an already existing element).
        private System.Collections.Generic.Dictionary<string, XMLElementData> _elements_added = new System.Collections.Generic.Dictionary<string, XMLElementData>();
        // Pending XML Element edits (any value edits, added attributes, or edited attributes).
        private System.Collections.Generic.Dictionary<string, XMLElementData> _elements_edits = new System.Collections.Generic.Dictionary<string, XMLElementData>();
        // Pending XML Element Attribute Deletions. If Element was made at runtime and not in the xml file,
        // remove it from the _elements_changed Dictionary instead.
        private System.Collections.Generic.Dictionary<string, XMLElementData> _element_attributes_deleted = new System.Collections.Generic.Dictionary<string, XMLElementData>();
        // Pending XML Element Deletions.
        private System.Collections.Generic.List<string> _elements_deleted = new System.Collections.Generic.List<string>();
        // Lock Saves so normal System.IO.File.ReadAllBytes
        // does not fail.
        private readonly object objLock = new object();

        /// <summary>
        /// Creates the XMLObject for reading and/or writing.
        ///
        /// If the file does not exists it will be created.
        /// </summary>
        /// <param name="xmlfilename">The name of the XML File to load into the XMLObject.</param>
        /// <param name="fallbackxmlcontent">The fallback content string to write
        /// into the fallback XML File if the file does not exist or if the file is empty.</param>
        public XMLObject(string xmlfilename, string fallbackxmlcontent)
            : this(xmlfilename, fallbackxmlcontent, false)
        {
        }

        /// <summary>
        /// Creates the XMLObject for reading and/or writing.
        ///
        /// If the file does not exists it will be created.
        /// </summary>
        /// <param name="xmlfilename">The name of the XML File to load into the XMLObject.</param>
        /// <param name="fallbackxmlcontent">The fallback content string to write into the fallback XML File if the file does not exist or if the file is empty.</param>
        /// <param name="SaveToAppStartFolder">Controls weather to save the file to the xmlfilename param string if it is the full path or to the Application Startup Path if it supplies file name only.</param>
        public XMLObject(string xmlfilename, string fallbackxmlcontent, bool SaveToAppStartFolder)
        {
            if (SaveToAppStartFolder)
            {
                if (!xmlfilename.Contains(System.Windows.Forms.Application.StartupPath))
                {
                    var newstr = System.Windows.Forms.Application.StartupPath + xmlfilename;
                    xmlfilename = newstr;
                }
            }
            if (!fallbackxmlcontent.Contains("<?xml version=\"1.0\" encoding=\"utf-8\" ?>"))
            {
                // insert root element at begginning of string data.
                fallbackxmlcontent = fallbackxmlcontent.Insert(0, "<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            }
            cached_xmlfilename = xmlfilename;
            Exists = System.IO.File.Exists(xmlfilename);
            HasChanged = !Exists;
            long fileSize = 0;
            if (Exists)
            {
                System.IO.FileInfo fileinfo = null;
                try
                {
                    fileinfo = new System.IO.FileInfo(xmlfilename);
                }
                catch (System.Exception)
                {
                    throw;
                }
                fileSize = fileinfo.Length;
            }
            doc = (fileSize > 0) ? System.Xml.Linq.XDocument.Load(xmlfilename) : System.Xml.Linq.XDocument.Parse(fallbackxmlcontent);
        }

        /// <summary>
        /// Reopens from the file name used to construct the object,
        /// but only if it has changed. If the file was not saved it
        /// will be saved first.
        /// </summary>
        public void ReopenFile()
        {
            Save();
            doc = System.Xml.Linq.XDocument.Load(cached_xmlfilename);
        }

        /// <summary>
        /// Adds or edits an attribute in an Element and sets it's value in the XMLObject.
        ///
        /// This method can also remove the attribute by setting the value to null.
        ///
        /// If Element does not exist yet it will be created automatically with an
        /// empty value as well as making the attribute as if the Element was
        /// pre-added before calling this function.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public void AddAttribute(string elementname, string attributename, object attributevalue)
        {
            if (disposedValue)
            {
                throw new System.ObjectDisposedException("XMLOblect is disposed.");
            }
            var elem = doc.Root.Element(elementname);
            if (elem == null)
            {
                Write(elementname, null);
            }
            if (_elements_added.ContainsKey(elementname))
            {
                var xMLAttributeData = new XMLAttributeData
                {
                    AttributeName = attributename,
                    value = attributevalue.ToString()
                };
                var xmleldata = _elements_added[elementname];
                xmleldata.Attributes = xmleldata.Attributes ?? new System.Collections.Generic.List<XMLAttributeData>();
                xmleldata.Attributes.Add(xMLAttributeData);
            }
            else if (_elements_edits.ContainsKey(elementname))
            {
                XMLAttributeData xMLAttributeData;
                var edit = false;
                var attributeIndex = 0;
                var xmleldata = _elements_edits[elementname];
                foreach (var attribute in xmleldata.Attributes)
                {
                    if (attribute.AttributeName.Equals(attributename))
                    {
                        edit = true;
                        attributeIndex = xmleldata.Attributes.IndexOf(attribute);
                    }
                }
                xMLAttributeData = new XMLAttributeData
                {
                    AttributeName = attributename,
                    value = attributevalue.ToString()
                };
                xmleldata.Attributes = xmleldata.Attributes ?? new System.Collections.Generic.List<XMLAttributeData>();
                if (!edit && attributevalue != null)
                {
                    xmleldata.Attributes.Add(xMLAttributeData);
                }
                else
                {
                    xMLAttributeData = xmleldata.Attributes[attributeIndex];
                    xMLAttributeData.value = attributevalue.ToString();
                }
            }
            else
            {
                // TODO: get if attribute is in document, if it is this determins
                if (attributevalue != null)
                {
                }
            }
            HasChanged = true;
        }

        /// <summary>
        /// Adds an Element to the XMLObject but verifies it does not exist first.
        /// </summary>
        /// <exception cref="System.Exception">Thrown if element already exists in the XMLObject.</exception>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        private void AddElement(string elementname, string value)
        {
            if (disposedValue)
            {
                throw new System.ObjectDisposedException("XMLOblect is disposed.");
            }
            var elem = doc.Root.Element(elementname);
            if (elem == null || !_elements_added.ContainsKey(elementname))
            {
                var xMLElementData = new XMLElementData
                {
                    Attributes = null,
                    value = value
                };
                _elements_added.Add(elementname, xMLElementData);
                HasChanged = true;
            }
            else
            {
                throw new System.Exception("Element already exists.");
            }
        }

        /// <summary>
        /// Writes to an element or updates it based upon the element name
        /// it is in and the value to place in it.
        ///
        /// If Element does not exist yet it will be created automatically.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public void Write(string elementname, string value)
        {
            if (disposedValue)
            {
                throw new System.ObjectDisposedException("XMLOblect is disposed.");
            }
            var elem = doc.Root.Element(elementname);
            if (elem != null || _elements_added.ContainsKey(elementname))
            {
                var xMLElementData = new XMLElementData
                {
                    Attributes = _elements_added.ContainsKey(elementname) ? _elements_added[elementname].Attributes : (_elements_edits.ContainsKey(elementname) ? _elements_edits[elementname].Attributes : null),
                    value = value
                };
                if (_elements_added.ContainsKey(elementname))
                {
                    // modify this key, do not put into _elements_edits dictonary.
                    _elements_added[elementname] = xMLElementData;
                }
                else
                {
                    if (_elements_edits.ContainsKey(elementname))
                    {
                        // edit the collection whenever this changes.
                        _elements_edits[elementname] = xMLElementData;
                    }
                    else
                    {
                        _elements_edits.Add(elementname, xMLElementData);
                    }
                }
                HasChanged = true;
            }
            else
            {
                AddElement(elementname, value);
            }
        }

        /// <summary>
        /// Writes to an attribute in an element or updates it based upon the element name
        /// it is in and the value to place in it.
        ///
        /// If Element does not exist yet it will be created automatically.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public void Write(string elementname, string attributename, string attributevalue)
        {
            if (disposedValue)
            {
                throw new System.ObjectDisposedException("XMLOblect is disposed.");
            }
            var elem = doc.Root.Element(elementname);
            if (elem != null || _elements_added.ContainsKey(elementname))
            {
            }
            else
            {
            }
        }

        /// <summary>
        /// Writes an array of elements to the parrent element or updates them based
        /// upon the element name.
        ///
        /// If Elements do not exist yet they will be created automatically.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public void Write(string parentelementname, string elementname, string[] values)
        {
        }

        /// <summary>
        /// Reads and returns the value set for an particular XML Element.
        ///
        /// If Element does not exist yet it will be created automatically with an empty value.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public string Read(string elementname)
        {
            if (disposedValue)
            {
                throw new System.ObjectDisposedException("XMLOblect is disposed.");
            }
            var elem = doc.Root.Element(elementname);
            if (elem != null)
            {
                // do not dare to look in _elements_deleted.
                return elem != null ? elem.Value : (_elements_edits.ContainsKey(elementname) ? _elements_edits[elementname].value : string.Empty);
            }
            else
            {
                Write(elementname, string.Empty);
                var output = _elements_added.ContainsKey(elementname) ? _elements_added[elementname].value : string.Empty;
                // if elementwas added before but was edited.
                output = (_elements_edits.ContainsKey(elementname) && output == string.Empty) ? _elements_edits[elementname].value : output;
                return output;
            }
        }

        /// <summary>
        /// Reads and returns the value set for an particular XML Element attribute.
        ///
        /// If Element and the attribute does not exist yet it will be created automatically
        /// with an empty value.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public string Read(string elementname, string attributename)
        {
            if (disposedValue)
            {
                throw new System.ObjectDisposedException("XMLOblect is disposed.");
            }
            var elem = doc.Root.Element(elementname);
            if (elem != null)
            {
                Write(elementname, attributename, string.Empty);
            }
            // until this is done.
            return string.Empty;
        }

        /// <summary>
        /// Reads and returns an array of values set for an particular XML Element's subelements.
        ///
        /// If Parent Element does not exist yet it will be created automatically
        /// with an empty value. In that case an empty string array is returned.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public string[] Read(string parentelementname, string elementname, object trash = null)
        {
            if (disposedValue)
            {
                throw new System.ObjectDisposedException("XMLOblect is disposed.");
            }
            var elem = doc.Root.Element(elementname);
            if (elem != null)
            {
            }
            else
            {
                Write(elementname, string.Empty);
            }
            // TODO: Read the subelements.
            return new string[] { };
        }

        /// <summary>
        /// Deletes an xml element using the element name.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <exception cref="System.ArgumentException">elementname does not exist in the xml.</exception>
        public void Delete(string elementname)
        {
            // before deleting the node (if it exists in xml), check if in any
            // of the dictionaries then add it to the dictionary for deleting values.
        }

        /// <summary>
        /// Removes an xml attribute using the element name and the name of the attribute.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <exception cref="System.ArgumentException">elementname or attributename does not exist in the xml.</exception>
        public void Delete(string elementname, string attributename)
        {
            // before deleting the node (if it exists in xml), check if in any
            // of the dictionaries then add it to the dictionary for deleting values.
        }

        /// <summary>
        /// Gets if the XML file existed when this object was created.
        ///
        /// This is just a property to minimize saving code on checking
        /// if the xml file changed externally.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        private bool Exists
        {
            get
            {
                if (disposedValue)
                {
                    throw new System.ObjectDisposedException("XMLOblect is disposed.");
                }
                return _exists;
            }
            set
            {
                if (disposedValue)
                {
                    throw new System.ObjectDisposedException("XMLOblect is disposed.");
                }
                _exists = value;
            }
        }

        /// <summary>
        /// Gets if the XML file was externally edited.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        private bool HasChangedExternally
        {
            get
            {
                if (disposedValue)
                {
                    throw new System.ObjectDisposedException("XMLOblect is disposed.");
                }
                var outxmlData = new System.IO.MemoryStream();
                doc.Save(outxmlData);
                var OutXmlBytes = outxmlData.ToArray();
                outxmlData.Dispose();
                // ensure file length is not 0.
                if (Exists != (System.IO.File.Exists(cached_xmlfilename) && System.Text.Encoding.UTF8.GetString(System.IO.File.ReadAllBytes(cached_xmlfilename)).Length > 0))
                {
                    // refresh Exists so it always works.
                    Exists = System.IO.File.Exists(cached_xmlfilename);
                }
                var dataOnFile = Exists ? System.IO.File.ReadAllBytes(cached_xmlfilename) : null;
                // cannot change externally if it does not exist on file yet.
                return dataOnFile == null ? false : !System.Linq.Enumerable.SequenceEqual(dataOnFile, OutXmlBytes) ? true : false;
            }
        }

        /// <summary>
        /// if the internal data in this class has changed, e.g.
        /// pending edits, deletions, or additions to the xml file.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        private bool HasChanged
        {
            get
            {
                if (disposedValue)
                {
                    throw new System.ObjectDisposedException("XMLOblect is disposed.");
                }
                return _changed;
            }
            set => _changed = value;
        }

        /// <summary>
        /// Saves the underlying XML file if it changed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">XMLOblect is disposed.</exception>
        public void Save()
        {
            lock (objLock)
            {
                if (disposedValue)
                {
                    throw new System.ObjectDisposedException("XMLOblect is disposed.");
                }
                if (HasChangedExternally && Exists)
                {
                    // reopen file to apply changes at runtime to it.
                    doc = System.Xml.Linq.XDocument.Load(cached_xmlfilename);
                }
                if (HasChanged)
                {
                    foreach (var added_elements in _elements_added)
                    {
                        // add elements to doc.
                        doc.Root.Add(new System.Xml.Linq.XElement(added_elements.Key, added_elements.Value.value));
                        if (added_elements.Value.Attributes != null)
                        {
                            // add attributes added to this element.
                            var elem = doc.Root.Element(added_elements.Key);
                            foreach (var attributes in added_elements.Value.Attributes)
                            {
                                elem.SetAttributeValue(attributes.AttributeName, attributes.value);
                            }
                            // add subelements and their attributes.
                            foreach (var element in added_elements.Value.Subelements)
                            {
                                // TODO: Add subelements to xml.
                            }
                        }
                    }
                    foreach (var edited_elements in _elements_edits)
                    {
                        var elem = doc.Root.Element(edited_elements.Key);
                        elem.Value = edited_elements.Value.value;
                        if (edited_elements.Value.Attributes != null)
                        {
                            // add/edit attributes added/edited to this element.
                            foreach (var attributes in edited_elements.Value.Attributes)
                            {
                                elem.SetAttributeValue(attributes.AttributeName, attributes.value);
                            }
                        }
                    }
                    foreach (var attributes_deleted in _element_attributes_deleted)
                    {
                        var elem = doc.Root.Element(attributes_deleted.Key);
                        // remove attributes on to this element.
                        foreach (var attributes in attributes_deleted.Value.Attributes)
                        {
                            elem.SetAttributeValue(attributes.AttributeName, attributes.value);
                        }
                    }
                    // hopefully this actually deletes the elements stored in this list.
                    foreach (var deleted_elements in _elements_deleted)
                    {
                        var elem = doc.Root.Element(deleted_elements);
                        elem.Remove();
                    }
                    // apply changes.
                    doc.Save(cached_xmlfilename);
                    _elements_added.Clear();
                    _elements_edits.Clear();
                    _element_attributes_deleted.Clear();
                    _elements_deleted.Clear();
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Save();
                }
                _exists = false;
                _changed = false;
                // remove everything from the Lists/Dictonaries then destroy them.
                _elements_added = null;
                _elements_edits = null;
                _element_attributes_deleted = null;
                _elements_deleted = null;
                doc = null;
                cached_xmlfilename = string.Empty;
                disposedValue = true;
            }
        }

        /// <summary>
        /// Disposes of the XMLObject and saves the underlying XML file if it changed.
        /// </summary>
        public void Dispose() => Dispose(true);
        #endregion
    }
}
