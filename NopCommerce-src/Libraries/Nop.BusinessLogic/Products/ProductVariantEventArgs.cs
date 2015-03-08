//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace NopSolutions.NopCommerce.BusinessLogic.Products
{
    /// <summary>
    /// Represents a product variant event arguments
    /// </summary>
    public partial class ProductVariantEventArgs : EventArgs
    {
        #region Ctor

        /// <summary>
        /// Creates a new instance of the ProductVariantEventArgs class
        /// </summary>
        public ProductVariantEventArgs()
        {
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the product variant
        /// </summary>
        public ProductVariant ProductVariant { get; set; }

        #endregion
    }
}
