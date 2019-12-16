using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using Umbraco.Core;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;
using Constants = Umbraco.Core.Constants;

namespace Umbraco.Controllers
{
    [Tree("content", "tablemanager", TreeTitle = "Tablemanager", SortOrder = 99)]
    public class TablemanagerTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
                menu.Items.Add(new CreateChildEntity(Services.TextService));
                menu.Items.Add(new RefreshNode(Services.TextService, true));
                return menu;
            }

            menu.Items.Add<ActionDelete>(Services.TextService, true, opensDialog: true);

            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            if (id == Constants.System.Root.ToInvariantString())
            {
                var favouriteThings = new Dictionary<int, string>
                {
                    { 1, "Raindrops on Roses" },
                    { 2, "Whiskers on Kittens" },
                    { 3, "Skys full of Stars" },
                    { 4, "Warm Woolen Mittens" },
                    { 5, "Cream coloured Unicorns" },
                    { 6, "Schnitzel with Noodles" }
                };
                
                var nodes = new TreeNodeCollection();

                foreach (var thing in favouriteThings)
                {
                    var node = CreateTreeNode(thing.Key.ToString(), "-1", queryStrings, thing.Value, "icon-presentation", false);
                    nodes.Add(node);
                }
                return nodes;
            }

            throw new NotSupportedException();
        }
    }
}
