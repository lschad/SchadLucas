using System;
using System.Windows;

namespace SchadLucas.Wpf.FontAwesome
{
    public partial class Brand
    {
        public static readonly DependencyProperty FaNameProperty =
                               DependencyProperty.Register(nameof(FaName), typeof(Icons.Brands), typeof(Brand),
                               new FrameworkPropertyMetadata(Icons.Brands.FontAwesome, FrameworkPropertyMetadataOptions.AffectsRender, OnUpdateControl));

        public Icons.Brands FaName
        {
            get => (Icons.Brands)GetValue(FaNameProperty);
            set => SetValue(FaNameProperty, value);
        }

        public Brand()
        {
            InitializeComponent();

            _placeholder.FontFamily = Fonts.Brands;
            _placeholder.Text = default;
        }

        private static void OnUpdateControl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Brand ctrl && e.Property == FaNameProperty)
            {
                var value = (Icons.Regular)Enum.ToObject(typeof(Icons.Regular), e.NewValue);
                var textBox = ctrl._placeholder;

                textBox.SetTextAndFamily(Fonts.Brands, (char)value);
            }
        }
    }
}