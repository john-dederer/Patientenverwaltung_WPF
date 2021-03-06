﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Patientenverwaltung_WPF
{
    public abstract class Datamodel
    {
        public string LastChange { get; set; }

        public void SetValues(Datamodel datamodel)
        {
            var dataModelFields = datamodel.GetType().GetProperties();
            var currentModelFiels = GetType().GetProperties();

            foreach (var field in dataModelFields)
                foreach (var fieldsCurrentModel in currentModelFiels)
                    if (field.Name.Equals(fieldsCurrentModel.Name))
                        fieldsCurrentModel.SetValue(this,field.GetValue(datamodel,null), null);
        }

        public void SetLogData()
        {
            LastChange = $@"{DateTime.Now.ToLocalTime()}|{CurrentContext.GetUser().Username}";
        }
               
    }
}