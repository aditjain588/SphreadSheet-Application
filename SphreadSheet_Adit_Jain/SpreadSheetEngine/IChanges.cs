// <copyright file="IChanges.cs" company="Adit Jain">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadSheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Cpts321;

    /// <summary>
    /// Change interfaces, which will be inherited by all the change classes.
    /// </summary>
    public interface IChanges
    {
        /// <summary>
        /// Execution of redo function.
        /// </summary>
        void ExecuteRedo();

        /// <summary>
        /// Execution of Undo function.
        /// </summary>
        void ExecuteUndo();
    }
}
