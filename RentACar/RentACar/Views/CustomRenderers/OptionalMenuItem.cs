using Prism.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RentACar.Views.CustomRenderers
{
    public partial class OptionalMenuItem : MenuItem, INotifyPropertyChanged
    {
        public bool NotHidden 
        { 
            get 
            { 
                return (bool)base.GetValue(NotHiddenProperty); 
            } 
            set 
            { 
                base.SetValue(NotHiddenProperty, value); 
            } 
        }

        public ICommand TrueCommand 
        {
            get
            {
                return (ICommand)base.GetValue(TrueCommandProperty);
            }
            set
            {
                base.SetValue(TrueCommandProperty, value);
            }
        }
        public ICommand FalseCommand 
        {
            get
            {
                return (ICommand)base.GetValue(FalseCommandProperty);
            }
            set
            {
                base.SetValue(FalseCommandProperty, value);
            }
        }

        public static readonly BindableProperty NotHiddenProperty =
            BindableProperty.Create(nameof(NotHidden),
                typeof(bool),
                typeof(OptionalMenuItem),
                defaultValue: true,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: HiddenPropertyChanged);

        public static readonly BindableProperty TrueCommandProperty =
            BindableProperty.Create(nameof(TrueCommand),
                typeof(DelegateCommand),
                typeof(OptionalMenuItem),
                null,
                propertyChanged: TrueCommandPropertyChanged);

        

        public static readonly BindableProperty FalseCommandProperty =
            BindableProperty.Create(nameof(FalseCommand),
                typeof(DelegateCommand),
                typeof(OptionalMenuItem),
                null,
                propertyChanged: FalseCommandPropertyChanged);

        private static void HiddenPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (OptionalMenuItem)bindable;
            control.NotHidden = (bool)newValue;
        }

        private static void TrueCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (OptionalMenuItem)bindable;
            control.TrueCommand = (DelegateCommand)newValue;
        }
        private static void FalseCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (OptionalMenuItem)bindable;
            control.FalseCommand = (DelegateCommand)newValue;
        }

        public static void Execute(ICommand command)
        {
            if (command == null) return;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
        public DelegateCommand OnTap => new DelegateCommand(() => {
            if (NotHidden)
            {
                Execute(TrueCommand);
            }
            else Execute(FalseCommand);
            });
    }
}
