using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;
using WITU.Extensions;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using NHibernate.Impl;
using Ninject;

namespace WITU.Utils
{
    public static class HtmlHelpers
    {
        [Inject]
        public static IGeneralHelper GeneralHelper = new GeneralHelper();

        public static MvcHtmlString DropDownWithModelSelectable<TModel, TValue>
            (this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,
             List<SelectListItem> items, object htmlAttributes = null)
        {
            return DropDownWithModelSelectable(helper, expression, items, null, htmlAttributes);
        }

        public static MvcHtmlString DropDownWithModelSelectable<TModel, TValue>
            (this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,
             List<SelectListItem> items, SelectListItem firstValue)
        {
            return DropDownWithModelSelectable(helper, expression, items, firstValue, null);
        }

        public static MvcHtmlString DropDownWithModelSelectable<TModel, TValue>
            (this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items,
                SelectListItem firstValue = null, object htmlAttributes = null)
        {
            if (items == null)
                items = new List<SelectListItem>();
            return DropDownWithModelSelectable(helper, expression, items.ToList(), firstValue, htmlAttributes);
        }

        public static MvcHtmlString DropDownWithModelSelectable<TModel, TValue>
            (this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, List<SelectListItem> items,
             SelectListItem firstValue = null, object htmlAttributes = null)
        {
            //Fetching the metadata related to expression. This includes property name, model value etc.
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            
            
//            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
//            //Fetching the property name from metadata object.
//            string propertyName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            string value = null;
            try
            {
                value = metadata.Model == null ? "" : metadata.Model.ToString();
            }
            catch (Exception)
            {


            }

            var name = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            //Created a select element which renderes the dropdown.
            TagBuilder dropdown = new TagBuilder("select");
            //Assigning the name and id attribute.
            dropdown.Attributes.Add("name", name);
            //dropdown.Attributes.Add("id", propertyName);

            //Created StringBuilder object to store option data fetched oen by one from list.
            StringBuilder options = new StringBuilder();

            //appending the first item (firstValue), if its not null...
            if (firstValue != null)
                options = options.Append("<option value='" + firstValue.Value + "'>" + firstValue.Text + "</option>");

            if (items != null)
            {
                //Iterated over the IEnumerable list.
                foreach (var item in (IEnumerable<SelectListItem>)items)
                {
                    if (item.Value.Equals(value))
                        options = options.Append("<option value='" + item.Value + "'selected='selected'>" + item.Text + "</option>");

                    else if (item.Selected)
                        options = options.Append("<option value='" + item.Value + "'selected='selected'>" + item.Text + "</option>");

                    //Each option represents a value in dropdown. 
                    //For each element in the list, option element is created and appended to the stringBuilder object.
                    else
                        options = options.Append("<option value='" + item.Value + "'>" + item.Text + "</option>");
                }
            }

            //assigned all the options to the dropdown using innerHTML property.    
            dropdown.InnerHtml = options.ToString();
            //Assigning the attributes passed as a htmlAttributes object.
            if(htmlAttributes != null)
                dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            //Returning the entire select or dropdown control in HTMLString format.
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString DateTimeEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper,
             Expression<Func<TModel, TValue>> expression, string format = null, object htmlAttributes = null)
        {
            //Fetching the metadata related to expression. This includes property name, model value etc.
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            //Fetching the property name from metadata object.
            var propertyName = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var input = new TagBuilder("input");
            //Assigning the name and id attribute.
            input.Attributes.Add("name", htmlFieldName);
            //input.Attributes.Add("id", propertyName);

            //Assigning the attributes passed as a htmlAttributes object.
            if (htmlAttributes != null)
                input.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            var value = (DateTime?)metadata.Model;
            if (value == null) return MvcHtmlString.Create(input.ToString(TagRenderMode.Normal));

            //NOW FOR THE ACTUAL DATE CONVERSION...
            var aValue = value.GetValueOrDefault(new DateTime());
            var textValue = "";
            if (aValue > DateTime.MinValue)
                textValue = format != null ? aValue.ToString(format) : aValue.ToString();

            input.Attributes.Add("value", textValue);

            //returning the element...
            return MvcHtmlString.Create(input.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString DropDownWithModelSelectable(this HtmlHelper helper, string value, List<SelectListItem> items,
             SelectListItem firstValue = null, object htmlAttributes = null)
        {
            
            //Created a select element which renderes the dropdown.
            var dropdown = new TagBuilder("select");

            //Assigning the name and id attribute.
            //dropdown.Attributes.Add("name", name);
            //dropdown.Attributes.Add("id", propertyName);

            //Created StringBuilder object to store option data fetched oen by one from list.
            var options = new StringBuilder();

            //appending the first item (firstValue), if its not null...
            if (firstValue != null)
                options = options.Append("<option value='" + firstValue.Value + "'>" + firstValue.Text + "</option>");

            //Iterated over the IEnumerable list.
            foreach (var item in (IEnumerable<SelectListItem>)items)
            {
                if (item.Value.Equals(value))
                    options = options.Append("<option value='" + item.Value + "'selected='selected'>" + item.Text + "</option>");

                else if (item.Selected)
                    options = options.Append("<option value='" + item.Value + "'selected='selected'>" + item.Text + "</option>");

                //Each option represents a value in dropdown. 
                //For each element in the list, option element is created and appended to the stringBuilder object.
                else
                    options = options.Append("<option value='" + item.Value + "'>" + item.Text + "</option>");
            }

            //assigned all the options to the dropdown using innerHTML property.    
            dropdown.InnerHtml = options.ToString();
            //Assigning the attributes passed as a htmlAttributes object.
            if (htmlAttributes != null)
                dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            //Returning the entire select or dropdown control in HTMLString format.
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Still incomplete. Alot more is required to fix any pending issues in this module. The following are my observations.
        /// Implement
        /// 1) Constraine the model to only accept Enum.
        /// 2) Pick the GeneralHelper to Assist in presenting the label which might be a Descriptor of the enum or the enum name.
        /// 3) Html Attribute to be effected on both the radio buttons and the group. More is required here.
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlRadioAttributes"></param>
        /// <param name="htmlGroupAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, object htmlRadioAttributes = null, object htmlGroupAttributes = null)
        {
            var attName = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var names = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();
            foreach (var name in names)
            {
                
                var id = string.Format("{0}_{1}_{2}", htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, 
                    metaData.PropertyName, name);

                //getting the actual name from the description, if available..
//                var nameOrDescription = G

                var radio = htmlHelper.RadioButtonFor(expression, name, htmlRadioAttributes ?? new { id = id, name = attName }).ToHtmlString();
                sb.AppendFormat("<label for=\"{0}\">{1}</label> {2}", id, radio, HttpUtility.HtmlEncode(name));
            }
            return MvcHtmlString.Create(sb.ToString());
        }


        /// <summary>
        /// Returns the Validation Summary which enables html encodng, a technique that the default Validation summary helper does not 
        /// provide.
        /// <para>
        ///     This code was picked from  <see href="http://stackoverflow.com/a/8158918/29407">here</see>. No changes were made to it.
        /// </para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlString UnEncodedValidationSummary(this HtmlHelper htmlHelper)
        {
            var formContextForClientValidation = htmlHelper.ViewContext.ClientValidationEnabled ? htmlHelper.ViewContext.FormContext : null;
            if (htmlHelper.ViewData.ModelState.IsValid)
            {
                if (formContextForClientValidation == null)
                {
                    return null;
                }
                if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    return null;
                }
            }

            var stringBuilder = new StringBuilder();
            var ulBuilder = new TagBuilder("ul");

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, out modelState))
            {
                foreach (ModelError error in modelState.Errors)
                {
                    string userErrorMessageOrDefault = error.ErrorMessage;
                    if (!string.IsNullOrEmpty(userErrorMessageOrDefault))
                    {
                        var liBuilder = new TagBuilder("li");
                        liBuilder.InnerHtml = userErrorMessageOrDefault;
                        stringBuilder.AppendLine(liBuilder.ToString(TagRenderMode.Normal));
                    }
                }
            }

            if (stringBuilder.Length == 0)
            {
                stringBuilder.AppendLine("<li style=\"display:none\"></li>");
            }
            ulBuilder.InnerHtml = stringBuilder.ToString();

            TagBuilder divBuilder = new TagBuilder("div");
            divBuilder.AddCssClass(htmlHelper.ViewData.ModelState.IsValid ? HtmlHelper.ValidationSummaryValidCssClassName : HtmlHelper.ValidationSummaryCssClassName);
            divBuilder.InnerHtml = ulBuilder.ToString(TagRenderMode.Normal);
            if (formContextForClientValidation != null)
            {
                if (!htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
                {
                    divBuilder.GenerateId("validationSummary");
                    formContextForClientValidation.ValidationSummaryId = divBuilder.Attributes["id"];
                    formContextForClientValidation.ReplaceValidationSummary = false;
                }
            }
            return new HtmlString(divBuilder.ToString(TagRenderMode.Normal));
        }

        public static string RadioButtonList(this HtmlHelper helper, string name, IEnumerable<string> items)
        {
            var selectList = new SelectList(items);
            return helper.RadioButtonList(name, selectList);
        }

        public static string RadioButtonList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items)
        {
            var tableTag = new TagBuilder("table");
            tableTag.AddCssClass("radio-main");

            var trTag = new TagBuilder("tr");
            foreach (SelectListItem item in items)
            {
                var tdTag = new TagBuilder("td");
                string rbValue = item.Value ?? item.Text;
                string rbName = name + "_" + rbValue;
                MvcHtmlString radioTag = helper.RadioButton(rbName, rbValue, item.Selected, new {name});

                var labelTag = new TagBuilder("label");
                labelTag.MergeAttribute("for", rbName);
                labelTag.MergeAttribute("id", rbName + "_label");
                labelTag.InnerHtml = rbValue;

                tdTag.InnerHtml = radioTag.ToHtmlString() + labelTag;

                trTag.InnerHtml += tdTag.ToString();
            }
            tableTag.InnerHtml = trTag.ToString();

            return tableTag.ToString();
        }

        public static IHtmlString RadioButtonGroup<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = null;
            try
            {
                value = metadata.Model == null ? "" : metadata.Model.ToString();
            }
            catch (Exception)
            {


            }

            //Getting the name of the tag...
            var name = helper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));

            var spanParentTag = new TagBuilder("span");
            var contentBuilder = new StringBuilder();
            foreach (var item in items)
            {
                var radioButton = new TagBuilder("input");
                radioButton.Attributes.Add("type", "radio");
                radioButton.Attributes.Add("name", name);

                if (htmlAttributes != null)
                    radioButton.MergeAttributes(new RouteValueDictionary(htmlAttributes));

                if(item.Value.Equals(value))
                    radioButton.Attributes.Add("checked", "checked");

                else if(item.Selected)
                    radioButton.Attributes.Add("checked", "checked");

                contentBuilder.Append(radioButton);
            }

            spanParentTag.InnerHtml = contentBuilder.ToString();

            //Returning the entire select or dropdown control in HTMLString format.
            return MvcHtmlString.Create(spanParentTag.ToString(TagRenderMode.Normal));
        }


        public static HtmlString DropDownEnumFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, SelectListItem defauItem = null, object htmlAttributes = null)
        {
            var typeOfProperty = expression.ReturnType;
            if (!typeOfProperty.IsEnum)
                throw new ArgumentException(string.Format("Type {0} is not an enum", typeOfProperty));

            //getting the enum that is defined..
            int? value = (int)ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
//            if (Enum.IsDefined(typeOfProperty, expression))
//            {
//                defaultValue = (int)ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
//            }

            var enumValues = new List<SelectListItem>();

            if (defauItem != null) //adding the default value before anything else...
            {
                if (value == null)
                    defauItem.Selected = true;

                enumValues.Add(defauItem);
            }

            //retrieves the elements..
            enumValues.AddRange(Enum.GetValues(typeOfProperty).Cast<Enum>().Select(x => new SelectListItem()
            {
                Value = (Convert.ToInt32(x)).ToString(),
                Text = GeneralHelper.GetEnumDescriptionIrName(x),
                Selected = ((int) value) == (Convert.ToInt32(x))
            }));

            

//            var selecteItem =  defaultValue != null
//                ? enumValues.FirstOrDefault(x => Convert.ToInt32(x.Value) == defaultValue)
//                : enumValues.First();

            return helper.DropDownListFor(expression, enumValues, htmlAttributes: htmlAttributes);
        }

        public static IHtmlString DropDownListForEnum<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var items =
                from value in values
                select new SelectListItem
                {
                    Text = GeneralHelper.GetEnumDescriptionIrName((Enum)Enum.Parse(enumType, value.ToString())),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                };

            return htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes);
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type modelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(modelType);
            if (underlyingType != null)
            {
                modelType = underlyingType;
            }
            return modelType;
        }

        /// <summary>
        /// Displays an enum value passed to it. If the value is not a valid enum, then an ArgumentException Error is thrown.
        /// <b>Usage: </b>
        /// <c>@Html.Display(m => m.Value)</c>
        /// where value in this case is the enum.
        /// <br/>
        /// The enum value is manipulated to return the enum actual value.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public static IHtmlString DisplayEnumFor<TModel, TValue>(this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> enumVal) where TValue : struct, IConvertible
        {
            //getting the enum value...
            if (!typeof(TValue).IsEnum)
                throw new ArgumentException("The value presented should be an enum");
            
            //var value = (int)ModelMetadata.FromLambdaExpression(ex, html.ViewData).Model;
            //string enumValue = Enum.GetName(enumType, value);
            //return new HtmlString(html.Encode(enumValue));

            var value = (int)ModelMetadata.FromLambdaExpression(enumVal, html.ViewData).Model;
            Type enumType = enumVal.GetType();

            if (value == null)
            {
                return new HtmlString("");
            }
            
            var enumActualVal = (Enum) Enum.Parse(enumType, value.ToString());
            var fullName = GeneralHelper.GetEnumDescriptionIrName(enumActualVal);

            return new HtmlString(html.Encode(fullName));
        }

        public static IHtmlString DisplayEnumFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> enumVal, Type enumType) where TValue : struct, IConvertible
        {
            //getting the enum value...
            if (!typeof(TValue).IsEnum)
                throw new ArgumentException("The value presented should be an enum");

            //var value = (int)ModelMetadata.FromLambdaExpression(ex, html.ViewData).Model;
            //string enumValue = Enum.GetName(enumType, value);
            //return new HtmlString(html.Encode(enumValue));

            var value = (int)ModelMetadata.FromLambdaExpression(enumVal, html.ViewData).Model;
           
            if (value == null)
            {
                return new HtmlString("");
            }

            var enumActualVal = (Enum)Enum.Parse(enumType, value.ToString());
            var fullName = GeneralHelper.GetEnumDescriptionIrName(enumActualVal);

            return new HtmlString(html.Encode(fullName));
        }

        public static IHtmlString DisplayEnumFor<TModel, TValue, T>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression, string optionLabel, IList<T> values, object htmlAttributes = null) where T : struct
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            //var values = Enum.GetValues(enumType).Cast<TValue>();
            var coreValues = values.Cast<TValue>();

            var items =
                from value in values
                select new SelectListItem
                {
                    Text = Enum.GetName(enumType, value),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                };
            
            return optionLabel != null ? htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes)
                : htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

        /// <summary>
        /// Dispalys an enum passed to it. Uses the HTML Helper to retrieve the type and works with the <see cref="IGeneralHelper"/> to
        /// retrieve the description of the application. 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IHtmlString DisplayEnum(this HtmlHelper html, Enum value)
        {
            if (value == null)
            {
                return new HtmlString(html.Encode(""));
            }
            var fullName = GeneralHelper.GetEnumDescriptionIrName(value);
            return new HtmlString(html.Encode(fullName));
        }

        /// <summary>
        /// Combines the value of the integer and enum to present the enum.
        /// Works with <see cref="DisplayEnum(System.Web.Mvc.HtmlHelper,System.Enum)"/>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IHtmlString DisplayEnum(this HtmlHelper html, int value, Type enumType)
        {
            var enumVal = (Enum)Enum.Parse(enumType, value.ToString());
            return DisplayEnum(html, enumVal);
        }

        public static IHtmlString DisplayCustomBoolean(this HtmlHelper html, bool value, string trueValue, string falseValue)
        {
            if(value)
                return  new HtmlString(html.Encode(trueValue));
            return new HtmlString(html.Encode(falseValue));
        }

        public static IHtmlString ReCaptcha(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            string publickey = WebConfigurationManager.AppSettings["RecaptchaPublicKey"];
            sb.AppendLine("<script type=\"text/javascript\"src='https://www.google.com/recaptcha/api.js'></script>");
            sb.AppendLine("");
            sb.AppendLine("<div class=\"g-recaptcha\"data-sitekey=\""+ publickey+"\"></div>");
        return MvcHtmlString.Create(sb.ToString()); 
        }

        public static string BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            // optional condition: I didn't wanted it to show on home and account controller
            if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Home" ||
                helper.ViewContext.RouteData.Values["controller"].ToString() == "Account")
            {
                return string.Empty;
            }

            StringBuilder breadcrumb = new StringBuilder("<div id=\"breadcrumbs-three\"><li>").Append(helper.ActionLink("Home", "Index", "Home").ToHtmlString()).Append("</li>");


            breadcrumb.Append("<li>");
            breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["controller"].ToString().Titleize(),
                                               "Index",
                                               helper.ViewContext.RouteData.Values["controller"].ToString()));
            breadcrumb.Append("</li>");

            if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
            {
                breadcrumb.Append("<li>");
                breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["action"].ToString().Titleize(),
                                                    helper.ViewContext.RouteData.Values["action"].ToString(),
                                                    helper.ViewContext.RouteData.Values["controller"].ToString()));
                breadcrumb.Append("</li>");
            }

            return breadcrumb.Append("</div><p></p>").ToString();
        }

        /// <summary>
        /// AutoComplete Text Box
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static MvcHtmlString AutocompleteTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName, object routeValues = null)
        {
            string autocompleteUrl = UrlHelper.GenerateUrl(null, actionName, controllerName,
                routeValues != null ?new RouteValueDictionary(routeValues):null,
                                                           html.RouteCollection,
                                                           html.ViewContext.RequestContext,
                                                           includeImplicitMvcValues: true);
            return html.TextBoxFor(expression, new { data_autocomplete_url = autocompleteUrl });
        } 
    }
}