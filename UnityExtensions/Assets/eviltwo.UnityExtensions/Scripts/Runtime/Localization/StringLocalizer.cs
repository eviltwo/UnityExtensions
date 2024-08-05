#if LOCALIZATION
using System;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace eviltwo.UnityExtensions
{
    [Obsolete("Use LocalizedString")]
    public class StringLocalizer : IDisposable
    {
        private readonly LocalizedStringTable _localizedStringTable;
        private readonly TableEntryReference _tableEntryReference;
        private object[] _args;
        public event Action<string> OnStringChanged;

        public StringLocalizer(LocalizedStringTable localizedStringTable, string key)
            : this(localizedStringTable, tableEntryReference: key)
        {
        }

        public StringLocalizer(LocalizedStringTable localizedStringTable, long keyId)
            : this(localizedStringTable, tableEntryReference: keyId)
        {
        }

        public StringLocalizer(LocalizedStringTable localizedStringTable, TableEntryReference tableEntryReference)
        {
            _localizedStringTable = localizedStringTable;
            _localizedStringTable.TableChanged += OnTableChanged;
            _tableEntryReference = tableEntryReference;
        }

        public void Dispose()
        {
            OnStringChanged = null;
            _localizedStringTable.TableChanged -= OnTableChanged;
        }

        public void SetArgs(params object[] args)
        {
            _args = args;
        }

        public string GetLocalizedString()
        {
            var table = _localizedStringTable.GetTable();
            return GetLocalizedString(table, _tableEntryReference, _args);
        }

        private void OnTableChanged(StringTable stringTable)
        {
            OnStringChanged?.Invoke(GetLocalizedString(stringTable, _tableEntryReference, _args));
        }

        private static string GetLocalizedString(StringTable stringTable, TableEntryReference entryRef, object[] args)
        {
            var entry = stringTable.GetEntryFromReference(entryRef);
            if (entry == null)
            {
                return null;
            }

            if (args == null || args.Length == 0)
            {
                return entry.GetLocalizedString();
            }
            else
            {
                return entry.GetLocalizedString(args);
            }
        }
    }
}
#endif
