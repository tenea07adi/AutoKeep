namespace MauiClient.UI.PartialViews;

public partial class DisplayFieldPartialView : ContentView
{
    public static readonly BindableProperty FieldNameProperty = 
        BindableProperty.Create(nameof(FieldName), typeof(string), typeof(DisplayFieldPartialView), string.Empty);

    public static readonly BindableProperty FielValueProperty =
        BindableProperty.Create(nameof(FielValue), typeof(string), typeof(DisplayFieldPartialView), string.Empty, BindingMode.OneWay, null,
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

    public string FielValue
    {
        get => (string)GetValue(FielValueProperty);
        set => SetValue(FielValueProperty, value);
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
            var result = FielValue;

            if(Format != string.Empty)
            {
                result = string.Format(Format, FielValue);
            }

            return result;
        }
    }

    public DisplayFieldPartialView()
	{
		InitializeComponent();
	}
}