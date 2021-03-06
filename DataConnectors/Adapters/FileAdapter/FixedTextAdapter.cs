﻿using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Serialization;
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

        public FixedTextAdapter(string filenName) : this()
        {
            this.FileName = filenName;
        }

        public FixedTextAdapter(Stream dataStream) : this()
        {
            this.DataStream = dataStream;
        }

        [XmlIgnore]
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

        [XmlAttribute]
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

        [XmlIgnore]
        public Stream DataStream
        {
            get
            {
                return this.fileAdapter.DataStream;
            }
            set
            {
                this.fileAdapter.DataStream = value;
            }
        }

        [XmlElement]
        public FieldDefinitionList FieldDefinitions
        {
            get { return (this.fileAdapter.ReadFormatter as FixedLengthToDataTableFormatter).FieldDefinitions; }
        }

        [XmlElement]
        public ValueConvertProcessor ReadConverter
        {
            get { return this.fileAdapter.ReadConverter; }
            set { this.fileAdapter.ReadConverter = value; }
        }

        [XmlElement]
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

        public void AutoDetectEncoding()
        {
            this.fileAdapter.AutoDetectEncoding();
        }

        public override void Dispose()
        {
            if (this.fileAdapter != null)
            {
                this.fileAdapter.Dispose();
            }
        }

        public IList<string> Validate()
        {
            return this.fileAdapter.Validate();
        }
    }
}