using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class UpperEntry : Entry
    {
        public UpperEntry()
        {
            this.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeCharacter);
            this.Behaviors.Add(new UpperEntryBehavior());
        }
    }

    public class UpperEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (args.NewTextValue != args.OldTextValue && !string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                ((Entry)sender).Text = args.NewTextValue.ToUpper();
            }
        }
    }
}
