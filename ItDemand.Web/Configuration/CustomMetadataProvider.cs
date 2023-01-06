using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

// How the model binder bind empty strings as empty and not null.
// https://stackoverflow.com/questions/44376540/asp-net-core-model-binding-how-to-get-empty-field-to-bind-as-a-blank-string

namespace ItDemand.Web.Configuration
{
    public class CustomMetadataProvider : IMetadataDetailsProvider, IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {

            if (context.Key.MetadataKind == ModelMetadataKind.Property)
            {

                context.DisplayMetadata.ConvertEmptyStringToNull = false;
            }
        }
    }
}
