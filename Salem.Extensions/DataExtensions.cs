using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Salem.Extensions {
    public static class DataExtensions {
        /// <summary>
        /// Synchronously loads all rows from the specified <see cref="SqlDataReader"/> into a new <see cref="DataTable"/>.
        /// </summary>
        /// <param name="reader">The <see cref="SqlDataReader"/> containing the data to load.</param>
        /// <returns>A <see cref="DataTable"/> populated with the data from the <see cref="SqlDataReader"/>.</returns>
        public static DataTable LoadIntoTable(this SqlDataReader reader) {
            var _dt = new DataTable();
            _dt.Load(reader);
            return _dt;
        }

        /// <summary>
        /// Loads data from the specified <see cref="SqlDataReader"/> into a <see cref="DataTable"/>. It does that in another thread while enabling you to await the result asyncronously.
        /// </summary>
        /// <param name="reader">The <see cref="SqlDataReader"/> instance containing the data to load.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="DataTable"/> populated with the
        /// data from the reader.</returns>
        public static async Task<DataTable> LoadIntoTableAsync(this SqlDataReader reader) {
            var _dt = new DataTable();
            await Task.Run(() => _dt.Load(reader));
            return _dt;
        }

        /// <summary>
        /// Prints the contents of the <see cref="DataTable"/> to the console in a formatted table.
        /// </summary>
        /// <param name="table">The table to print.</param>
        /// <param name="columnWidth">The width of each column in console characters.</param>
        /// <param name="printTitle">Whether to print the table name as a centered title.</param>
        /// <exception cref="ArgumentNullException">Thrown when the table parameter is <see langword="null"/>.</exception>
        public static void PrintToConsole(this DataTable table, int columnWidth, bool printTitle = true) {
            if (table is null)
                throw new ArgumentNullException(nameof(table), "The table cannot be null.");

            string _dashes = new string('-', table.Columns.Count * (columnWidth + 1));

            if (printTitle && table.TableName.Length > 0)
                Console.WriteLine(new string(' ', (_dashes.Length - table.TableName.Length) / 2) + table.TableName);

            Console.WriteLine(_dashes);

            foreach (DataColumn _col in table.Columns)
                Console.Write($" {_col.ToString().PadRight(columnWidth - 2)} |");

            Console.WriteLine($"\n{_dashes}");

            foreach (DataRow _row in table.Rows) {
                foreach (var _element in _row.ItemArray)
                    Console.Write($" {_element.ToString().PadRight(columnWidth - 2)} |");

                Console.WriteLine();
            }

            Console.WriteLine(_dashes);
        }
    }
}
