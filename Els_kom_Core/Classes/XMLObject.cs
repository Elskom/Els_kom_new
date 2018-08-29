// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml.Linq;

    /// <summary>
    /// Class in the Core that allows Reading and Writing of XML Files.
    /// </summary>
    public sealed class XMLObject : IDisposable
    {
        // TODO: Add functions to remove XML Entries and Attributes too.
        // TODO: Finish Read(string elementname, string attributename) and
        // Write(string elementname, string attributename, object attributevalue)
        // shortcut methods.
        // TODO: Add ways of adding, editing, and deleting elements within elements.
        // Lock Saves so normal System.IO.File.ReadAllBytes
        // does not fail.
        private bool exists = false;
        private bool changed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLObject"/> class
        /// for reading and/or writing.
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
        /// Initializes a new instance of the <see cref="XMLObject"/> class
        /// for reading and/or writing.
        ///
        /// If the file does not exists it will be created.
        /// </summary>
        /// <param name="xmlfilename">The name of the XML File to load into the XMLObject.</param>
        /// <param name="fallbackxmlcontent">The fallback content string to write into the fallback XML File if the file does not exist or if the file is empty.</param>
        /// <param name="saveToAppStartFolder">Controls weather to save the file to the xmlfilename param string if it is the full path or to the Application Startup Path if it supplies file name only.</param>
        public XMLObject(string xmlfilename, string fallbackxmlcontent, bool saveToAppStartFolder)
        {
            this.ObjLock = new object();
            this.ElementsAdded = new Dictionary<string, XMLElementData>();
            this.ElementsEdits = new Dictionary<string, XMLElementData>();
            this.ElementAttributesDeleted = new Dictionary<string, XMLElementData>();
            this.ElementsDeleted = new List<string>();
            if (saveToAppStartFolder)
            {
                if (!xmlfilename.Contains(Application.StartupPath))
                {
                    var newstr = Application.StartupPath + xmlfilename;
                    xmlfilename = newstr;
                }
            }

            if (!fallbackxmlcontent.Contains("<?xml version=\"1.0\" encoding=\"utf-8\" ?>"))
            {
                // insert root element at begginning of string data.
                fallbackxmlcontent = fallbackxmlcontent.Insert(0, "<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            }

            this.CachedXmlfilename = xmlfilename;
            this.Exists = File.Exists(xmlfilename);
            this.HasChanged = !this.Exists;
            long fileSize = 0;
            if (this.Exists)
            {
                FileInfo fileinfo = null;
                try
                {
                    fileinfo = new FileInfo(xmlfilename);
                }
                catch (Exception)
                {
                    throw;
                }

                fileSize = fileinfo.Length;
            }

            this.Doc = (fileSize > 0) ? XDocument.Load(xmlfilename) : XDocument.Parse(fallbackxmlcontent);
        }

        /// <summary>
        /// Gets a value indicating whether the XMLObject is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

        private object ObjLock { get; set; }

        private XDocument Doc { get; set; }

        private string CachedXmlfilename { get; set; }

        // Pending XML Element Addictions (excluding only adding attributes to an already existing element).
        private Dictionary<string, XMLElementData> ElementsAdded { get; set; }

        // Pending XML Element edits (any value edits, added attributes, or edited attributes).
        private Dictionary<string, XMLElementData> ElementsEdits { get; set; }

        // Pending XML Element Attribute Deletions. If Element was made at runtime and not in the xml file,
        // remove it from the _elements_changed Dictionary instead.
        private Dictionary<string, XMLElementData> ElementAttributesDeleted { get; set; }

        // Pending XML Element Deletions.
        private List<string> ElementsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the XML file existed when this object was created.
        ///
        /// This is just a property to minimize saving code on checking
        /// if the xml file changed externally.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        private bool Exists
        {
            get
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(XMLObject));
                }

                return this.exists;
            }

            set
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(XMLObject));
                }

                this.exists = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the XML file was externally edited.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        private bool HasChangedExternally
        {
            get
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(XMLObject));
                }

                var outxmlData = new MemoryStream();
                this.Doc.Save(outxmlData);
                var outXmlBytes = outxmlData.ToArray();
                outxmlData.Dispose();

                // ensure file length is not 0.
                if (this.Exists != (File.Exists(this.CachedXmlfilename) && Encoding.UTF8.GetString(File.ReadAllBytes(this.CachedXmlfilename)).Length > 0))
                {
                    // refresh Exists so it always works.
                    this.Exists = File.Exists(this.CachedXmlfilename);
                }

                var dataOnFile = this.Exists ? File.ReadAllBytes(this.CachedXmlfilename) : null;

                // cannot change externally if it does not exist on file yet.
                return dataOnFile == null ? false : !dataOnFile.SequenceEqual(outXmlBytes) ? true : false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the internal data in this class has changed, e.g.
        /// pending edits, deletions, or additions to the xml file.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        private bool HasChanged
        {
            get
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(XMLObject));
                }

                return this.changed;
            }
            set => this.changed = value;
        }

        /// <summary>
        /// Reopens from the file name used to construct the object,
        /// but only if it has changed. If the file was not saved it
        /// will be saved first.
        /// </summary>
        public void ReopenFile()
        {
            this.Save();
            this.Doc = XDocument.Load(this.CachedXmlfilename);
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
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <param name="elementname">The name of the element to add a attribute to.</param>
        /// <param name="attributename">The name of the attribute to add.</param>
        /// <param name="attributevalue">The value of the attribute.</param>
        public void AddAttribute(string elementname, string attributename, object attributevalue)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Root.Element(elementname);
            if (elem == null)
            {
                this.Write(elementname, null);
            }

            if (this.ElementsAdded.ContainsKey(elementname))
            {
                var xMLAttributeData = new XMLAttributeData
                {
                    AttributeName = attributename,
                    Value = attributevalue.ToString()
                };
                var xmleldata = this.ElementsAdded[elementname];
                xmleldata.Attributes = xmleldata.Attributes ?? new List<XMLAttributeData>();
                xmleldata.Attributes.Add(xMLAttributeData);
            }
            else if (this.ElementsEdits.ContainsKey(elementname))
            {
                XMLAttributeData xMLAttributeData;
                var edit = false;
                var attributeIndex = 0;
                var xmleldata = this.ElementsEdits[elementname];
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
                    Value = attributevalue.ToString()
                };
                xmleldata.Attributes = xmleldata.Attributes ?? new List<XMLAttributeData>();
                if (!edit && attributevalue != null)
                {
                    xmleldata.Attributes.Add(xMLAttributeData);
                }
                else
                {
                    xMLAttributeData = xmleldata.Attributes[attributeIndex];
                    xMLAttributeData.Value = attributevalue.ToString();
                }
            }
            else
            {
                // TODO: get if attribute is in document, if it is this determins
                if (attributevalue != null)
                {
                }
            }

            this.HasChanged = true;
        }

        /// <summary>
        /// Writes to an element or updates it based upon the element name
        /// it is in and the value to place in it.
        ///
        /// If Element does not exist yet it will be created automatically.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <param name="elementname">The name of the element to write to or create.</param>
        /// <param name="value">The value for the element.</param>
        public void Write(string elementname, string value)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Root.Element(elementname);
            if (elem != null || this.ElementsAdded.ContainsKey(elementname))
            {
                var xMLElementData = new XMLElementData
                {
                    Attributes = this.ElementsAdded.ContainsKey(elementname) ? this.ElementsAdded[elementname].Attributes : (this.ElementsEdits.ContainsKey(elementname) ? this.ElementsEdits[elementname].Attributes : null),
                    Value = value
                };
                if (this.ElementsAdded.ContainsKey(elementname))
                {
                    // modify this key, do not put into _elements_edits dictonary.
                    this.ElementsAdded[elementname] = xMLElementData;
                }
                else
                {
                    if (this.ElementsEdits.ContainsKey(elementname))
                    {
                        // edit the collection whenever this changes.
                        this.ElementsEdits[elementname] = xMLElementData;
                    }
                    else
                    {
                        this.ElementsEdits.Add(elementname, xMLElementData);
                    }
                }

                this.HasChanged = true;
            }
            else
            {
                this.AddElement(elementname, value);
            }
        }

        /// <summary>
        /// Writes to an attribute in an element or updates it based upon the element name
        /// it is in and the value to place in it.
        ///
        /// If Element does not exist yet it will be created automatically.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <param name="elementname">
        /// The name of the element to create an attribute or set an
        /// attribute in or to create with the attribute.
        /// </param>
        /// <param name="attributename">The attribute name to change the value or to create.</param>
        /// <param name="attributevalue">The value of the attribute to use.</param>
        public void Write(string elementname, string attributename, string attributevalue)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Root.Element(elementname);
            if (elem != null || this.ElementsAdded.ContainsKey(elementname))
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
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <param name="parentelementname">parrent element name of the subelement.</param>
        /// <param name="elementname">The name to use when writing subelement(s).</param>
        /// <param name="values">The array of values to use for the subelement(s).</param>
        public void Write(string parentelementname, string elementname, string[] values)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Root.Element(parentelementname);

            // TODO: Add Subelements to pending changes list.
        }

        /// <summary>
        /// Reads and returns the value set for an particular XML Element.
        ///
        /// If Element does not exist yet it will be created automatically with an empty value.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <param name="elementname">The element name to read the value from.</param>
        /// <returns>The value of the input element or <see cref="string.Empty"/>.</returns>
        public string Read(string elementname)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Root.Element(elementname);
            if (elem != null)
            {
                // do not dare to look in _elements_deleted.
                return elem != null ? elem.Value : (this.ElementsEdits.ContainsKey(elementname) ? this.ElementsEdits[elementname].Value : string.Empty);
            }
            else
            {
                this.Write(elementname, string.Empty);
                var output = this.ElementsAdded.ContainsKey(elementname) ? this.ElementsAdded[elementname].Value : string.Empty;

                // if elementwas added before but was edited.
                output = (this.ElementsEdits.ContainsKey(elementname) && output == string.Empty) ? this.ElementsEdits[elementname].Value : output;
                return output;
            }
        }

        /// <summary>
        /// Reads and returns the value set for an particular XML Element attribute.
        ///
        /// If Element and the attribute does not exist yet it will be created automatically
        /// with an empty value.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <param name="elementname">The element name to get the value of a attribute.</param>
        /// <param name="attributename">The name of the attribute to get the value of.</param>
        /// <returns>The value of the input element or <see cref="string.Empty"/>.</returns>
        public string Read(string elementname, string attributename)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Root.Element(elementname);
            if (elem == null)
            {
                this.Write(elementname, attributename, string.Empty);
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
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <param name="parentelementname">The name of the parrent element of the subelement(s)</param>
        /// <param name="elementname">The name of the subelements to get their values.</param>
        /// <param name="unused">
        /// A unused paramiter to avoid a compiler error from this overload.
        /// </param>
        /// <returns>
        /// A array of values or a empty array of strings if
        /// there is no subelements to this element.
        /// </returns>
        public string[] Read(string parentelementname, string elementname, object unused = null)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Descendants(parentelementname);
            var strarray = new string[] { };
            foreach (var element in elem)
            {
                strarray = element.Elements(elementname).Select(
                    y => (string)y).ToArray();
            }

            if (elem == XElement.EmptySequence)
            {
                this.Write(parentelementname, string.Empty);
            }

            return strarray;
        }

        /// <summary>
        /// Deletes an xml element using the element name.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <exception cref="ArgumentException">elementname does not exist in the xml or in pending edits.</exception>
        /// <param name="elementname">The element name of the element to delete.</param>
        public void Delete(string elementname)
        {
            // before deleting the node (if it exists in xml), check if in any
            // of the dictionaries then add it to the dictionary for deleting values.
        }

        /// <summary>
        /// Removes an xml attribute using the element name and the name of the attribute.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        /// <exception cref="ArgumentException">elementname or attributename does not exist in the xml or in pending edits.</exception>
        /// <param name="elementname">The element name that has the attribute to delete.</param>
        /// <param name="attributename">The name of the attribute to delete.</param>
        public void Delete(string elementname, string attributename)
        {
            // before deleting the attribute (if it exists in xml), check if in any
            // of the dictionaries then add it to the dictionary for deleting values.
        }

        /// <summary>
        /// Saves the underlying XML file if it changed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">XMLOblect is disposed.</exception>
        public void Save()
        {
            lock (this.ObjLock)
            {
                if (this.IsDisposed)
                {
                    throw new ObjectDisposedException(nameof(XMLObject));
                }

                if (this.HasChangedExternally && this.Exists)
                {
                    // reopen file to apply changes at runtime to it.
                    this.Doc = XDocument.Load(this.CachedXmlfilename);
                }

                if (this.HasChanged)
                {
                    foreach (var added_elements in this.ElementsAdded)
                    {
                        // add elements to doc.
                        this.Doc.Root.Add(new XElement(added_elements.Key, added_elements.Value.Value));
                        if (added_elements.Value.Attributes != null)
                        {
                            // add attributes added to this element.
                            var elem = this.Doc.Root.Element(added_elements.Key);
                            foreach (var attributes in added_elements.Value.Attributes)
                            {
                                elem.SetAttributeValue(attributes.AttributeName, attributes.Value);
                            }

                            // add subelements and their attributes.
                            foreach (var element in added_elements.Value.Subelements)
                            {
                                this.SaveAddedSubelements(elem, element);
                            }
                        }
                    }

                    foreach (var edited_elements in this.ElementsEdits)
                    {
                        var elem = this.Doc.Root.Element(edited_elements.Key);
                        elem.Value = edited_elements.Value.Value;
                        if (edited_elements.Value.Attributes != null)
                        {
                            // add/edit attributes added/edited to this element.
                            foreach (var attributes in edited_elements.Value.Attributes)
                            {
                                elem.SetAttributeValue(attributes.AttributeName, attributes.Value);
                            }
                        }
                    }

                    foreach (var attributes_deleted in this.ElementAttributesDeleted)
                    {
                        var elem = this.Doc.Root.Element(attributes_deleted.Key);

                        // remove attributes on to this element.
                        foreach (var attributes in attributes_deleted.Value.Attributes)
                        {
                            elem.SetAttributeValue(attributes.AttributeName, attributes.Value);
                        }
                    }

                    // hopefully this actually deletes the elements stored in this list.
                    foreach (var deleted_elements in this.ElementsDeleted)
                    {
                        var elem = this.Doc.Root.Element(deleted_elements);
                        elem.Remove();
                    }

                    // apply changes.
                    this.Doc.Save(this.CachedXmlfilename);
                    this.ElementsAdded.Clear();
                    this.ElementsEdits.Clear();
                    this.ElementAttributesDeleted.Clear();
                    this.ElementsDeleted.Clear();

                    // avoid unneeded writes if nothing changed after this.
                    this.HasChanged = false;
                }
            }
        }

        /// <summary>
        /// Disposes of the XMLObject and saves the underlying XML file if it changed.
        /// </summary>
        public void Dispose() => this.Dispose(true);

        // Summary:
        //   Adds an Element to the XMLObject but verifies it does not exist first.
        //
        // Exceptions:
        //   System.Exception:
        //     Thrown if element already exists in the XMLObject.
        //   System.ObjectDisposedException
        //     XMLOblect is disposed.
        private void AddElement(string elementname, string value)
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(XMLObject));
            }

            var elem = this.Doc.Root.Element(elementname);
            if (elem == null || !this.ElementsAdded.ContainsKey(elementname))
            {
                var xMLElementData = new XMLElementData
                {
                    Attributes = null,
                    Value = value
                };
                this.ElementsAdded.Add(elementname, xMLElementData);
                this.HasChanged = true;
            }
            else
            {
                throw new Exception("Element already exists.");
            }
        }

        // Summary:
        //   Writes Added subelements to the XML file.
        private void SaveAddedSubelements(XElement xElement, XMLElementData elemdata)
        {
            if (elemdata.Name != string.Empty)
            {
                var elem = new XElement(elemdata.Name, elemdata.Value);
                xElement.Add(elem);
                if (elemdata.Attributes != null)
                {
                    foreach (var attributes in elemdata.Attributes)
                    {
                        elem.SetAttributeValue(attributes.AttributeName, attributes.Value);
                    }
                }

                if (elemdata.Subelements != null)
                {
                    // recursively add each subelement of these subelements.
                    foreach (var element in elemdata.Subelements)
                    {
                        this.SaveAddedSubelements(elem, element);
                    }
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (!this.IsDisposed)
            {
                if (disposing)
                {
                    this.Save();
                }

                this.exists = false;
                this.changed = false;

                // remove everything from the Lists/Dictonaries then destroy them.
                this.ElementsAdded = null;
                this.ElementsEdits = null;
                this.ElementAttributesDeleted = null;
                this.ElementsDeleted = null;
                this.Doc = null;
                this.CachedXmlfilename = string.Empty;
                this.IsDisposed = true;
            }
        }
    }
}
