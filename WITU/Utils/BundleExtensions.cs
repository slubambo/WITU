using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WITU.Utils
{
    public static class BundleExtensions
    {
        /// <summary>
        /// Includes the specified <paramref name="virtualPaths"/> within the bundle and attached the
        /// <see cref="System.Web.Optimization.CssRewriteUrlTransform"/> item transformer to each item
        /// automatically.
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        /// <param name="virtualPaths">The virtual paths.</param>
        /// <returns>Bundle.</returns>
        /// <exception cref="System.ArgumentException">Only available to StyleBundle;bundle</exception>
        /// <exception cref="System.ArgumentNullException">virtualPaths;Cannot be null or empty</exception>
        public static Bundle IncludeWithCssRewriteTransform(this Bundle bundle, params String[] virtualPaths)
        {
            if (!(bundle is StyleBundle))
            {
                throw new ArgumentException("Only available to StyleBundle", "bundle");
            }
            if (virtualPaths == null || virtualPaths.Length == 0)
            {
                throw new ArgumentNullException("virtualPaths", "Cannot be null or empty");
            }
            IItemTransform itemTransform = new CssRewriteUrlTransform();
            foreach (String virtualPath in virtualPaths)
            {
                if (!String.IsNullOrWhiteSpace(virtualPath))
                {
                    bundle.Include(virtualPath, itemTransform);
                }
            }
            return bundle;
        }
    }
}