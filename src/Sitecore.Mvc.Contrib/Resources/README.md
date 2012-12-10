h2. Resource Manager

To use the Resource Manager with MVC you need to wrap it in another static class:

    public static class ErrorResources 
    {
        static ErrorResources()
        {

            if (Manager == null)
                Manager = new SitecoreResourceManager("/sitecore/system/Dictionary", true);
        }

        private static SitecoreResourceManager Manager { get; set; }

        public static string RequiredComment
        {
            get { return Manager.GetString("RequiredComment"); }
        }
        public static string RequiredFullName
        {
            get { return Manager.GetString("RequiredFullName"); }
        }

    }


You can point the Resource Manager at either an item where it will load every field as a resource or at a dictionary, it will then assume the children of the targeted item are dictionary items.

You must specify static members that either match the field name or the dictionary key to be used. This static members are required by the .NET MVC framework.

You can make resource members editable by using the HTML extension Sitecore.Mvc.Contrib.Html.ScLabel: 

The Model:

<pre>
    public class CommentItem
    {
        [SitecoreField]
        [Display(ResourceType = typeof(ErrorItemResources), Name = "ContentLabel")]
        [Required(ErrorMessageResourceName = "RequiredComment", ErrorMessageResourceType = typeof(ErrorItemResources))]
        public virtual string Content { get; set; }

        
    }
</pre>

The view:

<pre>
@Html.ScLabelFor(x=>x.Content)
</pre>
