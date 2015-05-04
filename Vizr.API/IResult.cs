﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public interface IResult
    {
        /// <summary>
        /// Globally unique hash to identify this particular Result
        /// </summary>
        Hash ID { get; set; }

        /// <summary>
        /// Display text for IResult in list
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Reference to parent ResultProvider which created it
        /// </summary>
        IResultProvider Provider { get; set; }

        /// <summary>
        /// Criteria upon which IResultScorer will score each IResult against a query
        /// </summary>
        IEnumerable<SearchableText> SearchableText { get; }

        /// <summary>
        /// Fired when user has the result selected and executes it. Returns false if launching failed.
        /// </summary>
        bool Launch();

        /// <summary>
        /// Fired when user has the result selected and requests more options on it
        /// </summary>
        void Options();

        /// <summary>
        /// Fired when user has the result selected and requests to edit it
        /// </summary>
        void Edit();

        /// <summary>
        /// Fired when user has the result selected and requests to remove it from being seen
        /// </summary>
        void Delete();
    }
}
