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
        private System.Xml.Linq.XDocument doc;
        private string cached_xmlfilename;
        // Lock Saves so normal System.IO.File.ReadAllBytes
        // does not fail.
        private object objLock = new object();

        /// <summary>
        /// Creates the XMLObject for reading and/or writing.
        ///
        /// If the file does not exists it will be created.
        /// </summary>
        /// <param name="xmlfilename">The name of the XML File to load into the XMLObject.</param>
        /// <param name="fallbackxmlcontent">The fallback content string to write into the fallback XML File if the file does not exist or if the file is empty.</param>
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
                    string newstr = System.Windows.Forms.Application.StartupPath + xmlfilename;
                    xmlfilename = newstr;
                }
            }
            cached_xmlfilename = xmlfilename;
            if (System.IO.File.Exists(xmlfilename))
            {
                byte[] xmldata = System.IO.File.ReadAllBytes(xmlfilename);
                if (xmldata.Length > 0)
                {
                    System.IO.MemoryStream xmlDataStream = new System.IO.MemoryStream(xmldata, true);
                    doc = System.Xml.Linq.XDocument.Load(xmlDataStream);
                    xmlDataStream.Dispose();
                }
                else
                {
                    doc = System.Xml.Linq.XDocument.Parse(fallbackxmlcontent);
                }
            }
            else
            {
                doc = System.Xml.Linq.XDocument.Parse(fallbackxmlcontent);
            }
        }

        /// <summary>
        /// Reopens from the file name used to construct the object,
        /// but only if it has changed. If the file was not saved it
        /// will be saved first.
        /// </summary>
        public void ReopenFile()
        {
            Save();
            byte[] xmldata = System.IO.File.ReadAllBytes(cached_xmlfilename);
            System.IO.MemoryStream xmlDataStream = new System.IO.MemoryStream(xmldata, true);
            doc = System.Xml.Linq.XDocument.Load(xmlDataStream);
            xmlDataStream.Dispose();
        }

        /// <summary>
        /// Adds an attribute to an Element and sets it's value in the XMLObject.
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
            System.Xml.Linq.XElement elem = doc.Root.Element(elementname);
            if (elem == null)
            {
                Write(elementname, null);
                elem = doc.Root.Element(elementname);
            }
            elem.SetAttributeValue(attributename, attributevalue);
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
            System.Xml.Linq.XElement elem = doc.Root.Element(elementname);
            if (elem == null)
            {
                doc.Root.Add(new System.Xml.Linq.XElement(elementname, value));
            }
            else
            {
                throw new System.Exception("Element already exists.");
            }
        }

        /// <summary>
        /// Writes an setting or updates it based upon the element name
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
            System.Xml.Linq.XElement elem = doc.Root.Element(elementname);
            if (elem != null)
            {
                elem.Value = value;
            }
            else
            {
                AddElement(elementname, value);
            }
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
            System.Xml.Linq.XElement elem = doc.Root.Element(elementname);
            if (elem != null)
            {
                return elem.Value;
            }
            else
            {
                Write(elementname, null);
                return doc.Root.Element(elementname).Value;
            }
        }

        /// <summary>
        /// Gets if the xml file has changed.
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
                System.IO.MemoryStream outxmlData = new System.IO.MemoryStream();
                doc.Save(outxmlData);
                byte[] OutXmlBytes = outxmlData.ToArray();
                if (System.IO.File.Exists(cached_xmlfilename))
                {
                    byte[] dataOnFile = System.IO.File.ReadAllBytes(cached_xmlfilename);
                    if (!System.Linq.Enumerable.SequenceEqual(dataOnFile, OutXmlBytes))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
                return false;
            }
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
                System.IO.MemoryStream outxmlData = new System.IO.MemoryStream();
                doc.Save(outxmlData);
                byte[] OutXmlBytes = outxmlData.ToArray();
                if (HasChanged)
                {
                    System.IO.FileStream fstream = System.IO.File.Create(cached_xmlfilename);
                    fstream.Write(OutXmlBytes, 0, OutXmlBytes.Length);
                    fstream.Dispose();
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
                doc = null;
                cached_xmlfilename = string.Empty;
                disposedValue = true;
            }
        }

        /// <summary>
        /// Disposes of the XMLObject and saves the underlying XML file if it changed.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
