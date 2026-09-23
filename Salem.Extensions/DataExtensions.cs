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
    }
}
