namespace MauiClient.UI.PartialViews;

public partial class DisplayFieldPartialView : ContentView
{
    public static readonly BindableProperty FieldNameProperty = 
        BindableProperty.Create(nameof(FieldName), typeof(string), typeof(DisplayFieldPartialView), string.Empty);

    public static readonly BindableProperty FieldValueProperty =
        BindableProperty.Create(nameof(FieldValue), typeof(string), typeof(DisplayFieldPartialView), string.Empty, BindingMode.OneWay, null,
            propertyChanged: (b, o, n) =>
            {
                var view = (DisplayFieldPartialView)b;

                view.OnPropertyChanged(nameof(FormatedValue));
            });

    public static readonly BindableProperty FormatProperty =
        BindableProperty.Create(nameof(Format), typeof(string), typeof(DisplayFieldPartialView), string.Empty, BindingMode.OneWay, null, 
            propertyChanged: (b, o, n) =>
                {
                    var view = (DisplayFieldPartialView)b;

                    view.OnPropertyChanged(nameof(FormatedValue));
                });

    public string FieldName
	{
		get => (string)GetValue(FieldNameProperty);
		set => SetValue(FieldNameProperty, value);
    }

    public string FieldValue
    {
        get => (string)GetValue(FieldValueProperty);
        set => SetValue(FieldValueProperty, value);
    }

    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    public string FormatedValue
    {
        get
        {
            var result = FieldValue;

            if(Format != string.Empty)
            {
                result = string.Format(Format, FieldValue);
            }

            return result;
        }
    }

    public DisplayFieldPartialView()
	{
		InitializeComponent();
	}
}