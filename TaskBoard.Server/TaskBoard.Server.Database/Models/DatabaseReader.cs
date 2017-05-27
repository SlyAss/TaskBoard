using System;
using System.Linq;
using System.Linq.Expressions;

namespace TaskBoard.Server.Database.Models {
	public abstract class DatabaseReader {
		protected readonly ModelDatabase ModelDatabase;

		protected DatabaseReader(ModelDatabase modelDatabase) {
			ModelDatabase = modelDatabase;
		}

		protected static void UseFilter<TTable>(bool useFilter, ref IQueryable<TTable> table, Expression<Func<TTable, bool>> predicate) {
			table = useFilter ? table.Where(predicate) : table;
		}
	}
}