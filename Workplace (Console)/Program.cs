using Salem.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Workspace__Console_ {
    internal class Program {
        internal const string CONNECTION_STRING = "Server=.;Database=C21_DB1;User Id=sa;Password=sa123456";

        static void Main() {
            // Run one of the following lines.

            RunManualCSharpDemo();
            RunSqlDemo();
        }

        internal static void RunManualCSharpDemo() {
            using (var _empTable = SelectTable("SELECT * FROM Employees2"))
            using (var _resultTable = ProcessTable(_empTable))
                _resultTable.PrintToConsole(22);
        }

        internal static void RunSqlDemo() {
            const string QUERY = @" SELECT 
                                    	PerformanceCategory, 
                                    	NumberOfEmployees = COUNT(*), 
                                    	AverageSalary = AVG(Salary) 
                                    FROM (
                                    	SELECT Name, Salary,
                                    		CASE
                                    			WHEN PerformanceRating >= 80 THEN 'High'
                                    			WHEN PerformanceRating >= 60 THEN 'Medium'
                                    			ELSE 'Low'
                                    		END AS PerformanceCategory
                                    	FROM Employees2
                                    	) As PerformanceTable
                                    GROUP BY PerformanceCategory";

            using (var _resultTable = SelectTable(QUERY))
                _resultTable.PrintToConsole(22);
        }

        internal static DataTable ProcessTable(DataTable empTable) {
            int _rating;
            (int _highCount, int _medCount, int _lowCount) = (0, 0, 0);
            (int _totalSalaryHigh, int _totalSalaryMed, int _totalSalaryLow) = (0, 0, 0);

            foreach (DataRow _row in empTable.Rows) {
                _rating = (int) _row["PerformanceRating"];

                if (_rating >= 80) { // High
                    _highCount++;
                    _totalSalaryHigh += (int) _row["Salary"];
                }
                else if (_rating >= 60) { // Medium
                    _medCount++;
                    _totalSalaryMed += (int) _row["Salary"];
                }
                else { // Low
                    _lowCount++;
                    _totalSalaryLow += (int) _row["Salary"];
                }
            }

            var _result = new DataTable();

            _result.Columns.Add("PerformanceCategory", typeof(string));
            _result.Columns.Add("TotalEmployees", typeof(int));
            _result.Columns.Add("AverageSalary", typeof(int));

            if (_highCount > 0)
                _result.Rows.Add("High", _highCount, _totalSalaryHigh / _highCount);

            if (_medCount > 0)
                _result.Rows.Add("Medium", _medCount, _totalSalaryMed / _medCount);

            if (_lowCount > 0)
                _result.Rows.Add("Low", _lowCount, _totalSalaryLow / _lowCount);

            return _result;
        }

        internal static DataTable SelectTable(string query) {
            using (var _connection = new SqlConnection(CONNECTION_STRING))
            using (var _command = new SqlCommand(query, _connection)) {
                try {
                    _connection.Open();

                    using (var _reader = _command.ExecuteReader())
                        return _reader.LoadIntoTable();
                } catch (Exception ex) {
                    Console.WriteLine($"Exception thrown: {ex.Message}");
                    return null;
                }
            }
        }
    }
}

