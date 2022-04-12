using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Reflection;

public class GeneralGetter : MonoBehaviour{
    private string nameField;
    private string nameProperty;

    public void PropertyName(string name) { nameProperty = name; }
    public void FieldName(string name){ nameField = name; } //Just to be used in the inspector

    public void SetReceptorField(object receptor, string nameFieldReceptor){
        Type typeThisClass = this.GetType();
        Type typeReceptor = receptor.GetType();

        FieldInfo thisField = typeThisClass.GetField(nameField);
        FieldInfo receptorField = typeReceptor.GetField(nameFieldReceptor);

        receptorField = thisField;
        receptorField.SetValue(receptor, thisField.GetValue(this));
    }

    public void SetReceptorProperty(object receptor, string namePropertyReceptor){
        Type typeThisClass = this.GetType();
        Type typeReceptor = receptor.GetType();

        PropertyInfo thisProperty = typeThisClass.GetProperty(nameProperty);
        PropertyInfo receptorProperty = typeReceptor.GetProperty(namePropertyReceptor);

        receptorProperty.SetValue(receptor, thisProperty.GetValue(this, null));
    }

}
