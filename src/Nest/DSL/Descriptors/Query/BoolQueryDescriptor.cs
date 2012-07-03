﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
  [JsonObject(MemberSerialization=MemberSerialization.OptIn)]
  public class BoolBaseQueryDescriptor
  {
    [JsonProperty("must")]
    internal IEnumerable<BaseQuery> _MustQueries { get; set; }

    [JsonProperty("should")]
    internal IEnumerable<BaseQuery> _ShouldQueries { get; set; }
  }
	


	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
  public class BoolQueryDescriptor<T> where T : class
	{
		[JsonProperty("must")]
		internal IEnumerable<QueryDescriptor<T>> _MustQueries { get; set; }

		[JsonProperty("must_not")]
		internal IEnumerable<QueryDescriptor<T>> _MustNotQueries { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<QueryDescriptor<T>> _ShouldQueries { get; set; }

		[JsonProperty("minimum_number_should_match")]
		internal int? _MinimumNumberShouldMatches { get; set; }

		[JsonProperty("boost")]
		internal double? _Boost { get; set; }

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumNumberShouldMatch(int minimumShouldMatches)
		{
			this._MinimumNumberShouldMatches = minimumShouldMatches;
			return this;
		}
		public BoolQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params Action<QueryDescriptor<T>>[] filters)
		{
			var descriptors = new List<QueryDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new QueryDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustQueries = descriptors;
			return this;
		}
		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. The minimum number of should clauses to match can be set using minimum_number_should_match parameter.
		/// </summary>
		/// <param name="filters"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params Action<QueryDescriptor<T>>[] filters)
		{
			var descriptors = new List<QueryDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new QueryDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._MustNotQueries = descriptors;
			return this;
		}
		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="filters"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params Action<QueryDescriptor<T>>[] filters)
		{
			var descriptors = new List<QueryDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new QueryDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this._ShouldQueries = descriptors;
			return this;
		}
	}
}
