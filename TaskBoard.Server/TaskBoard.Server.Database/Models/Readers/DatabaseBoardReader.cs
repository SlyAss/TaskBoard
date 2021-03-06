﻿using System;
using System.Linq;
using TaskBoard.Common.Database.Readers;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;
using TaskBoard.Server.Database.Extensions;

namespace TaskBoard.Server.Database.Models.Readers {
	// ReSharper disable UnusedMember.Global

	public class DatabaseBoardReader : DatabaseReader, IDatabaseBoardReader {
		public DatabaseBoardReader(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public Board GetById(Guid id) {
			return ModelDatabase.GetBoard(id).ToTable();
		}

		public Guid[] GetAllIds() {
			return ModelDatabase.Boards.Select(board => board.BoardId).ToArray();
		}

		public Board[] GetAll() {
			return ModelDatabase.Boards.ToTables();
		}

		public Guid[] GetIdsWithUsingFilters(string name) {
			return GetQueryWithUsingFilters(name).Select(board => board.BoardId).ToArray();
		}

		public Board[] GetWithUsingFilters(string name) {
			return GetQueryWithUsingFilters(name).ToTables();
		}

		private IQueryable<BoardEntity> GetQueryWithUsingFilters(string name) {
			IQueryable<BoardEntity> boards = ModelDatabase.Boards;
			UseFilter(name != null, ref boards, board => board.Name.Contains(name));

			return boards;
		}
	}
}