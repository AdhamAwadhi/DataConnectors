﻿using System.Collections.Generic;
using System.Data;
using System.Text;
using DataConnectors.Common.Model;
using DataConnectors.Converters;
using DataConnectors.Formatters;

namespace DataConnectors.Adapter.FileAdapter
{
    public class FixedTextAdapter : DataAdapterBase
    {
        private readonly FlatFileAdapter fileAdapter = new FlatFileAdapter();

        public FixedTextAdapter()
        {
            this.fileAdapter.ReadFormatter = new FixedLengthToDataTableFormatter();
            this.fileAdapter.WriteFormatter = new DataTableToFixedLengthFormatter();

            // set to the same reference
            (this.fileAdapter.ReadFormatter as FixedLengthToDataTableFormatter).FieldDefinitions = (this.fileAdapter.WriteFormatter as DataTableToFixedLengthFormatter).FieldDefinitions;
        }

        public Encoding Encoding
        {
            get
            {
                return this.fileAdapter.Encoding;
            }

            set
            {
                this.fileAdapter.Encoding = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileAdapter.FileName;
            }

            set
            {
                this.fileAdapter.FileName = value;
            }
        }

        public FieldDefinitionList FieldDefinitions
        {
            get { return (this.fileAdapter.ReadFormatter as FixedLengthToDataTableFormatter).FieldDefinitions; }
        }

        public ValueConvertProcessor ReadConverter
        {
            get { return this.fileAdapter.ReadConverter; }
            set { this.fileAdapter.ReadConverter = value; }
        }

        public ValueConvertProcessor WriteConverter
        {
            get { return this.fileAdapter.WriteConverter; }
            set { this.fileAdapter.WriteConverter = value; }
        }

        public override IList<DataColumn> GetAvailableColumns()
        {
            return this.fileAdapter.GetAvailableColumns();
        }

        public override IList<string> GetAvailableTables()
        {
            return this.fileAdapter.GetAvailableTables();
        }

        public override int GetCount()
        {
            return this.fileAdapter.GetCount();
        }

        public override IEnumerable<DataTable> ReadData(int? blockSize = null)
        {
            return this.fileAdapter.ReadData(blockSize);
        }

        public override bool WriteData(IEnumerable<DataTable> tables, bool deleteBefore = false)
        {
            return this.fileAdapter.WriteData(tables, deleteBefore);
        }
    }
}