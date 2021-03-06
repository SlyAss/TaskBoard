﻿using System;
using TaskBoard.Common.Database.Editors;
using TaskBoard.Common.Tables;
using TaskBoard.Server.Database.Entities;

namespace TaskBoard.Server.Database.Models.Editors {
	// ReSharper disable UnusedMember.Global

	public class DatabaseCommentEditor : DatabaseEditor, IDatabaseCommentEditor {
		public DatabaseCommentEditor(ModelDatabase modelDatabase) : base(modelDatabase) {
		}

		public void Add(Comment table) {
			ModelDatabase.Comments.Add(new CommentEntity {
				CommentId = Guid.NewGuid(),
				Content = table.Content,
				CreateDateTime = DateTime.Now,
				UserId = ModelDatabase.GetUser(table.UserId).UserId,
				TaskId = ModelDatabase.GetTask(table.TaskId).TaskId
			});

			ModelDatabase.SaveChanges();
		}

		public void Edit(Guid oldTableId, Comment newTable) {
			var comment = ModelDatabase.GetComment(oldTableId);
			comment.Content = newTable.Content;

			ModelDatabase.SaveChanges();
		}

		public void Delete(Guid tableId) {
			DeleteComment(tableId);
			ModelDatabase.SaveChanges();
		}
	}
}