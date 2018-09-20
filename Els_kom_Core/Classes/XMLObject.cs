// Copyright (c) 2014-2018, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom_Core.Classes
{
    using System;
    using XmlAbstraction;

    /// <summary>
    /// Class in the Core that allows Reading and Writing of XML Files.
    /// </summary>
    public sealed class XMLObject
    {
        private readonly XmlObject xmlObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLObject"/> class
        /// for reading xml data from memory.
        ///
        /// With this contstructor, the <see cref="XMLObject"/> returned will be read-only.
        /// </summary>
        /// <param name="xmlcontent">The xml data to load into memory.</param>
        public XMLObject(string xmlcontent)
            => this.xmlObject = new XmlObject(xmlcontent);

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLObject"/> class
        /// for reading and/or writing.
        ///
        /// If the file does not exists it will be created.
        /// </summary>
        /// <param name="xmlfilename">
        /// The name of the XML File to load into the XMLObject.
        /// </param>
        /// <param name="fallbackxmlcontent">
        /// The fallback content string to write into the fallback XML File
        /// if the file does not exist or if the file is empty.
        /// </param>
        public XMLObject(string xmlfilename, string fallbackxmlcontent)
            => this.xmlObject = new XmlObject(xmlfilename, fallbackxmlcontent);

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLObject"/> class
        /// for reading and/or writing.
        ///
        /// If the file does not exists it will be created.
        /// </summary>
        /// <param name="xmlfilename">
        /// The name of the XML File to load into the XMLObject.
        /// </param>
        /// <param name="fallbackxmlcontent">
        /// The fallback content string to write into the fallback XML File
        /// if the file does not exist or if the file is empty.
        /// </param>
        /// <param name="saveToAppStartFolder">
        /// Controls weather to save the file to the xmlfilename param string if
        /// it is the full path or to the Application Startup Path if it supplies file name only.
        /// </param>
        public XMLObject(string xmlfilename, string fallbackxmlcontent, bool saveToAppStartFolder)
            => this.xmlObject = new XmlObject(xmlfilename, fallbackxmlcontent, saveToAppStartFolder);

        /// <summary>
        /// Reopens from the file name used to construct the object,
        /// but only if it has changed. If the file was not saved it
        /// will be saved first.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="InvalidOperationException">Cannot reopen on read-only instances.</exception>
        public void ReopenFile() => this.xmlObject.ReopenFile();

        /// <summary>
        /// Adds or edits an attribute in an Element and sets it's value in the <see cref="XMLObject"/>.
        ///
        /// This method can also remove the attribute by setting the value to null.
        ///
        /// If Element does not exist yet it will be created automatically with an
        /// empty value as well as making the attribute as if the Element was
        /// pre-added before calling this function.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="Exception">Attribute already exists in the xml file.</exception>
        /// <exception cref="InvalidOperationException">When called from a read-only instance.</exception>
        /// <param name="elementname">The name of the element to add a attribute to.</param>
        /// <param name="attributename">The name of the attribute to add.</param>
        /// <param name="attributevalue">The value of the attribute.</param>
        public void AddAttribute(string elementname, string attributename, object attributevalue)
            => this.xmlObject.AddAttribute(elementname, attributename, attributevalue);

        /// <summary>
        /// Writes to an element or updates it based upon the element name
        /// it is in and the value to place in it.
        ///
        /// If Element does not exist yet it will be created automatically.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="InvalidOperationException">When called from a read-only instance.</exception>
        /// <param name="elementname">The name of the element to write to or create.</param>
        /// <param name="value">The value for the element.</param>
        public void Write(string elementname, string value) => this.xmlObject.Write(elementname, value);

        /// <summary>
        /// Writes to an attribute in an element or updates it based upon the element name
        /// it is in and the value to place in it.
        ///
        /// If Element does not exist yet it will be created automatically.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="Exception">Attribute already exists in the xml file.</exception>
        /// <exception cref="InvalidOperationException">When called from a read-only instance.</exception>
        /// <param name="elementname">
        /// The name of the element to create an attribute or set an
        /// attribute in or to create with the attribute.
        /// </param>
        /// <param name="attributename">The attribute name to change the value or to create.</param>
        /// <param name="attributevalue">The value of the attribute to use.</param>
        public void Write(string elementname, string attributename, string attributevalue)
            => this.xmlObject.Write(elementname, attributename, attributevalue);

        /// <summary>
        /// Writes an array of elements to the parrent element or updates them based
        /// upon the element name.
        ///
        /// If Elements do not exist yet they will be created automatically.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="InvalidOperationException">When called from a read-only instance.</exception>
        /// <param name="parentelementname">parrent element name of the subelement.</param>
        /// <param name="elementname">The name to use when writing subelement(s).</param>
        /// <param name="values">The array of values to use for the subelement(s).</param>
        public void Write(string parentelementname, string elementname, string[] values)
            => this.xmlObject.Write(parentelementname, elementname, values);

        /// <summary>
        /// Reads and returns the value set for an particular XML Element.
        ///
        /// If Element does not exist yet it will be created automatically with an empty value.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="InvalidOperationException">When the Element does not exist in a read-only instance.</exception>
        /// <param name="elementname">The element name to read the value from.</param>
        /// <returns>The value of the input element or <see cref="string.Empty"/>.</returns>
        public string Read(string elementname) => this.xmlObject.Read(elementname);

        /// <summary>
        /// Reads and returns the value set for an particular XML Element attribute.
        ///
        /// If Element and the attribute does not exist yet it will be created automatically
        /// with an empty value.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="InvalidOperationException">When the Element does not exist in a read-only instance.</exception>
        /// <param name="elementname">The element name to get the value of a attribute.</param>
        /// <param name="attributename">The name of the attribute to get the value of.</param>
        /// <returns>The value of the input element or <see cref="string.Empty"/>.</returns>
        public string Read(string elementname, string attributename)
            => this.xmlObject.Read(elementname, attributename);

        /// <summary>
        /// Reads and returns an array of values set for an particular XML Element's subelements.
        ///
        /// If Parent Element does not exist yet it will be created automatically
        /// with an empty value. In that case an empty string array is returned.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="InvalidOperationException">When the Element does not exist in a read-only instance.</exception>
        /// <param name="parentelementname">The name of the parrent element of the subelement(s).</param>
        /// <param name="elementname">The name of the subelements to get their values.</param>
        /// <param name="unused">
        /// A unused paramiter to avoid a compiler error from this overload.
        /// </param>
        /// <returns>
        /// A array of values or a empty array of strings if
        /// there is no subelements to this element.
        /// </returns>
        public string[] Read(string parentelementname, string elementname, object unused = null)
            => this.xmlObject.Read(parentelementname, elementname, unused);

        /// <summary>
        /// Deletes an xml element using the element name.
        /// Can also delete not only the parrent element but also subelements with it.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="ArgumentException">elementname does not exist in the xml or in pending edits.</exception>
        /// <exception cref="InvalidOperationException">When the object is a read-only instance.</exception>
        /// <param name="elementname">The element name of the element to delete.</param>
        public void Delete(string elementname) => this.xmlObject.Delete(elementname);

        /// <summary>
        /// Removes an xml attribute using the element name and the name of the attribute.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        /// <exception cref="ArgumentException">elementname or attributename does not exist in the xml or in pending edits.</exception>
        /// <exception cref="InvalidOperationException">When the object is a read-only instance.</exception>
        /// <param name="elementname">The element name that has the attribute to delete.</param>
        /// <param name="attributename">The name of the attribute to delete.</param>
        public void Delete(string elementname, string attributename)
            => this.xmlObject.Delete(elementname, attributename);

        /// <summary>
        /// Saves the underlying XML file if it changed.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><see cref="XMLObject"/> is disposed.</exception>
        public void Save() => this.xmlObject.Save();
    }
}
