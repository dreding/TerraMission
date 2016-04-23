using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

public abstract class NotifyPropertyChanged : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue))
            return false;

        field = newValue;
        RaisePropertyChanged(propertyExpression);
        return true;
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, e);
    }

    protected void RaisePropertyChanged(string propertyName)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
        var propertyName = GetPropertyName(propertyExpression);
        RaisePropertyChanged(propertyName);
    }

    protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
        if (propertyExpression == null)
            throw new ArgumentNullException("propertyExpression");

        var body = propertyExpression.Body as MemberExpression;
        if (body == null)
            throw new ArgumentException("Invalid argument", "propertyExpression");

        var property = body.Member as PropertyInfo;
        if (property == null)
            throw new ArgumentException("Argument is not a property", "propertyExpression");

        return property.Name;
    }
}
