// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted.
        /// </summary>
        private int ColumnToSort;

        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder OrderOfSort;

        /// <summary>
        /// Case insensitive comparer object.
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListViewColumnSorter"/> class.
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            this.ColumnToSort = 0;

            // Initialize the sort order to 'none'
            this.OrderOfSort = SortOrder.None;

            this.SortByDate = false;

            // Initialize the CaseInsensitiveComparer object
            this.ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// Gets or sets a value indicating whether to sort as a date.
        /// </summary>
        /// <value>
        /// A value indicating whether to sort as a date.
        /// </value>
        public bool SortByDate { get; set; }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        /// <value>
        /// The number of the column to which to apply the sorting operation (Defaults to '0').
        /// </value>
        public int SortColumn
        {
            get => this.ColumnToSort;
            set => this.ColumnToSort = value;
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        /// <value>
        /// The order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </value>
        public SortOrder Order
        {
            get => this.OrderOfSort;
            set => this.OrderOfSort = value;
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared.</param>
        /// <param name="y">Second object to be compared.</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'.</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // Compare the two items
            compareResult = this.SortByDate
                ? DateTime.Compare((DateTime)listviewX.SubItems[this.ColumnToSort].Tag, (DateTime)listviewY.SubItems[this.ColumnToSort].Tag)
                : this.ObjectCompare.Compare(listviewX.SubItems[this.ColumnToSort].Text, listviewY.SubItems[this.ColumnToSort].Text);

            // Calculate correct return value based on object comparison
            if (this.OrderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (this.OrderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return -compareResult;
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }
    }
}
