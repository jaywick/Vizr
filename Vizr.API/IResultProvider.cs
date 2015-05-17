﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public interface IResultProvider
    {
        /// <summary>
        /// Unique name of provider such as 'com.example.myprovider'
        /// </summary>
        string UniqueName { get; set; }

        /// <summary>
        /// FontAwesome icon key to display on a result
        /// </summary>
        string Icon { get; set; }

        /// <summary>
        /// Called when the application is loaded in the background, for example on system start
        /// </summary>
        void OnBackgroundStart();

        /// <summary>
        /// Called when application is brought onto the screen, for example when hotkey is pressed
        /// </summary>
        void OnAppStart();

        /// <summary>
        /// Called when application is closed from the screen
        /// </summary>
        void OnAppHide();

        /// <summary>
        /// Called when user changes query text
        /// </summary>
        void OnQueryChange(string queryText);

        /// <summary>
        /// Called when preferences are changed
        /// </summary>
        void OnPreferencesUpdated();

        /// <summary>
        /// List of all IResults from this provider
        /// </summary>
        /// <param name="message">Search query from the user</param>
        /// <returns>List of Result based on input <paramref name="message"/></returns>
        IEnumerable<IResult> Items { get; set; }
    }
}
