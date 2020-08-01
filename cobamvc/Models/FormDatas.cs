using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cobamvc.Models
{


    public class FormInput
    {
        public int index { get; set; }
        public const string typeC = "INPUT";
        public string type { get { return typeC; } }
        public dynamic data { get; set; } //string || number
        public string label { get; set; }
        public string name { get; set; }
        public bool disabled { get; set; }
        public bool required { get; set; }
        public string requiredWhen { get; set; } // "APPROVE" || "NOT_APPROVE" || ("BOTH" || null)
        public string typeForm { get; set; } // "text" || "number"
    }


    public class FormFile
    {
        public int index { get; set; }
        public const string typeC = "FILE";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public List<FormFileItem> data { get; set; }
    }

    public class FormFileItem
    {
        public string label { get; set; }
        public string fileName { get; set; }
        public string link { get; set; }
        public string typeDoc { get; set; }
    }

    public class FormDate
    {
        public int index { get; set; }
        public const string typeC = "DATE";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public string name { get; set; }
        public bool disabled { get; set; }
        public bool required { get; set; }
        public string requiredWhen { get; set; } // "APPROVE" || "NOT_APPROVE" || ("BOTH" || null)
        public bool range { get; set; } // To indicate this date has start and end
        public List<DateTime> data { get; set; } // max length is 2. they are start date and end date
    }

    public class FormCheckBox
    {
        public int index { get; set; }
        public const string typeC = "CHECKBOX";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public string name { get; set; }
        public bool required { get; set; }
        public string requiredWhen { get; set; } // "APPROVE" || "NOT_APPROVE" || ("BOTH" || null)
        public List<FormCheckBoxItem> data { get; set; }
    }

    public class FormCheckBoxItem
    {
        public string label { get; set; }
        public string data { get; set; }
        public bool disabled { get; set; }
        public bool checkSign { get; set; }
    }



    public class FormTextArea
    {
        public int index { get; set; }
        public const string typeC = "TEXTAREA";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public string name { get; set; }
        public bool required { get; set; }
        public string requiredWhen { get; set; } // "APPROVE" || "NOT_APPROVE" || ("BOTH" || null)
        public bool disabled { get; set; }
        public string data { get; set; }
    }

    public class FormRadio
    {
        public int index { get; set; }
        public const string typeC = "RADIO";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public string name { get; set; }
        public bool required { get; set; }
        public string requiredWhen { get; set; } // "APPROVE" || "NOT_APPROVE" || ("BOTH" || null)
        public bool disabled { get; set; }
        public string initialValue { get; set; }
        public List<FormRadioItem> data { get; set; }
    }
    public class FormRadioItem
    {   
        public bool disabled { get; set; }
        public string data { get; set; }
        public string label { get; set; }
    }

    public class FormSelect
    {
        public int index { get; set; }
        public const string typeC = "SELECT";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public string name { get; set; }
        public bool required { get; set; }
        public string requiredWhen { get; set; } // "APPROVE" || "NOT_APPROVE" || ("BOTH" || null)
        public bool disabled { get; set; }
        public string initialValue { get; set; }
        public List<FormSelectItem> data { get; set; }
    }
    public class FormSelectItem
    {
        public bool disabled { get; set; }
        public string data { get; set; }
        public string label { get; set; }
    }

    public class FormList
    {
        public int index { get; set; }
        public const string typeC = "LIST";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public List<string> data { get; set; }
    }
    public class FormTable
    {
        public int index { get; set; }
        public const string typeC = "TABLE";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public List<dynamic> data { get; set; }
        public List<FormTableHeader> header { get; set; }
    }

    public class FormTableHeader
    {
        public string key { get; set; }
        public string title { get; set; }
    }

    public class FormGroup
    {
        public int index { get; set; }
        public string title { get; set; }
        public const string typeC = "FORMGROUP";
        public string type { get { return typeC; } }
    }

    public class FormAutoComplete
    {

        public int index { get; set; }
        public const string typeC = "AUTOCOMPLETE";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public string name { get; set; }
        public bool required { get; set; }
        public string requiredWhen { get; set; } // "APPROVE" || "NOT_APPROVE" || ("BOTH" || null)
        public bool disabled { get; set; }
        public string initialValue { get; set; }
        public List<FormAutoCompleteItem> data { get; set; }
        public string link { get; set; } // references to get data items
        public bool provideFilter { get; set; }// this will use label as query param on that link. e.g link?label=an or link?.yourparam...&label=an
                                               // if not, filtering will be handle by general approval
                                               // method must be GET
    }
    public class FormAutoCompleteItem
    {
        public string data { get; set; }
        public string label { get; set; }
    }

    public class FormImage
    {
        public int index { get; set; }
        public const string typeC = "IMAGE";
        public string type { get { return typeC; } }
        public string label { get; set; }
        public string link { get; set; } // references to get image
        public string fileName { get; set; }
    }

    public class Detail
    {
        public string title { get; set; }
        public string subtitle { get; set; }
        public string link { get; set; }
        public string urlProject { get; set; }
        public List<dynamic> data { get; set; }
    }


}