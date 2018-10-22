using System;
using System.Windows;

namespace SchadLucas.Wpf.FontAwesome
{
    public partial class Icon
    {
        //public static readonly DependencyProperty FaStyleProperty =
        //                       DependencyProperty.Register(nameof(FaStyle), typeof(Styles), typeof(Icon),
        //                       new FrameworkPropertyMetadata(Styles.Regular, FrameworkPropertyMetadataOptions.AffectsRender, OnUpdateControl));

        public static readonly DependencyProperty FaNameProperty =
                               DependencyProperty.Register(nameof(FaName), typeof(Icons.Regular), typeof(Icon),
                               new FrameworkPropertyMetadata(Icons.Regular.FontAwesomeLogoFull, FrameworkPropertyMetadataOptions.AffectsRender, OnUpdateControl));

        //public Styles FaStyle
        //{
        //    get => (Styles)GetValue(FaStyleProperty);
        //    set => SetValue(FaStyleProperty, value);
        //}
        public Icons.Regular FaName
        {
            get => (Icons.Regular)GetValue(FaNameProperty);
            set => SetValue(FaNameProperty, value);
        }

        public Icon()
        {
            InitializeComponent();

            _placeholder.FontFamily = Fonts.Regular;
            _placeholder.Text = default;
        }

        private static void OnUpdateControl(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Icon ctrl && e.Property == FaNameProperty)
            {
                var value = (Icons.Regular)Enum.ToObject(typeof(Icons.Regular), e.NewValue);
                var textBox = ctrl._placeholder;
                var font = Fonts.Regular;

                //switch (ctrl.FaStyle)
                //{
                //    case Styles.Regular:
                //        font = Fonts.Regular;
                //        break;

                //    case Styles.Solid:
                //        font = Fonts.Solid;
                //        break;
                //}

                textBox.SetTextAndFamily(font, (char)value);
            }
        }
    }
}