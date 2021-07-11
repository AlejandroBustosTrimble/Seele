using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class LimitedEntry : Entry
    {

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
    propertyName: "MaxLength",
    returnType: typeof(int),
    declaringType: typeof(LimitedEntry),
    defaultValue: 999,
    defaultBindingMode: BindingMode.TwoWay
    );

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public LimitedEntry()
        {
            this.Behaviors.Add(new LimitedEntryBehavior(this.MaxLength));
        }
    }

    public class LimitedEntryBehavior : Behavior<LimitedEntry>
    {
        int maxi = 0;
        public LimitedEntryBehavior(int max)
        {
            maxi = max;
        }

        protected override void OnAttachedTo(LimitedEntry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(LimitedEntry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (LimitedEntry)sender;

            if (entry.Text.Length > entry.MaxLength)
            {
                string entryText = entry.Text;

                entryText = entryText.Remove(entryText.Length - 1);

                entry.Text = entryText;
            }
        }
    }
}
