using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionModel> GetAllQuestionList()
        {
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var result = context.questionmasters.OrderBy(s => s.QuestionId).Where(o => o.IsActive == "Y");

                var data = result.Select(s => new QuestionModel()
                {
                    id = s.id,
                    QuestionId = (int)s.QuestionId,
                    Question = s.Question
                }).ToList();
                return data;
            }
        }

        public QuestionModel AddQuestion(QuestionModel model)
        {
            QuestionModel response = new QuestionModel();
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                var qestionid = context.questionmasters.OrderByDescending(o => o.CreatedOn).Select(p => p.QuestionId).FirstOrDefault();
                questionmaster data = new questionmaster();
                
                data.QuestionId = qestionid + 1;
                data.Question = model.Question;
                data.StoreId = model.StoreId;
                data.IsActive =model.IsActive;
                context.questionmasters.Add(data);
                var result = context.SaveChanges();
                int id = data.id;
                if (result > 0)
                {
                    var resultData = context.questionmasters.Where(s => s.id == id).FirstOrDefault();
                    response.id = resultData.id;
                    response.QuestionId = (int)resultData.QuestionId;
                    response.Question = resultData.Question;
                    response.StoreId = (int)resultData.StoreId;
                    response.IsActive = resultData.IsActive;
                }
            }
            return response;
        }


        //public ResponseStatus DeleteQuestionByID(int id)
        //{
        //    ResponseStatus response = new ResponseStatus();
        //    using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
        //    {
        //        //questionmaster data = new questionmaster();
        //        var data = context.questionmasters.Find(id);
        //        if (data != null)
        //        {
        //            context.questionmasters.Remove(data);
        //            context.SaveChanges();
        //            response.isSuccess = true;
        //            response.serverResponseTime = System.DateTime.Now;
        //        }
        //        else
        //        {
        //            response.isSuccess = false;
        //            response.serverResponseTime = System.DateTime.Now;
        //        }
        //    }
        //    return response;
        //}

        public QuestionModel QuestionDisableEnable(QuestionModel model)
        {
            QuestionModel response = new QuestionModel();
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {
                questionmaster data = new questionmaster();

                data.id = model.id;
                data.StoreId = model.StoreId;
                data.IsActive = model.IsActive;
                context.questionmasters.Attach(data);
                context.Entry(data).Property(x => x.IsActive).IsModified = true;
                var result = context.SaveChanges();
                int id = data.id;
                if (result > 0)
                {
                    var resultData = context.questionmasters.Where(s => s.id == id).Select(t => new QuestionModel()
                    {
                        id = t.id,
                        QuestionId = (int)t.QuestionId,
                        Question = t.Question,
                        StoreId = (int)t.StoreId,
                        IsActive = t.IsActive
                    }).FirstOrDefault();
                    response = resultData;
                }
            }
            return response;
        }

        public QuestionModel QuestionUpdate(QuestionModel model)
        {
            QuestionModel data = new QuestionModel();
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {

                questionmaster response = new questionmaster();

                response.StoreId = model.StoreId;
                response.QuestionId = model.QuestionId;
                response.id = model.id;
                response.Question = model.Question;
                response.IsActive = model.IsActive;

                context.Entry(response).State = System.Data.Entity.EntityState.Modified;
                var result = context.SaveChanges();
                int id = response.id;
                if (result > 0)
                {

                    var resultData = context.questionmasters.Where(s => s.id == id).FirstOrDefault();
                    if (resultData != null)
                    {


                        data.id = resultData.id;
                        data.QuestionId = (int)resultData.QuestionId;
                        data.Question = resultData.Question;
                        data.StoreId = (int)resultData.StoreId;
                        data.IsActive = resultData.IsActive;
                    }
                }
            }
            return data;

        }

        //public QuestionModel RearrangeQuestions(QuestionModel model)
        //{
        //    QuestionModel response = new QuestionModel();
        //    using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
        //    {
        //        questionmaster data = new questionmaster();
        //        data.QuestionId = model.QuestionId;
        //        data.Question = model.Question;
        //        data.StoreId = model.StoreId;
        //        //data.IsActive = model.IsActive;
        //        context.questionmasters.Add(data);
        //        var result = context.SaveChanges();
        //        int id = data.id;
        //        if (result > 0)
        //        {
        //            var resultData = context.questionmasters.Where(s => s.id == id).FirstOrDefault();
        //            response.id = resultData.id;
        //            response.QuestionId = (int)resultData.QuestionId;
        //            response.Question = resultData.Question;
        //            response.StoreId = (int)resultData.StoreId;
        //            response.IsActive = resultData.IsActive;
        //        }
        //    }
        //    return response;
        //}
        public IEnumerable<QuestionModel> ArrangeQuestion(int questionid, int positionid)
        {
            IEnumerable<QuestionModel> response = null;
            using (shamsweetsfeedback_androidEntities context = new shamsweetsfeedback_androidEntities())
            {              
                if (questionid > 0)
                {
                    var moved = context.questionmasters.Where(q => q.QuestionId == positionid).FirstOrDefault();
                    if (moved != null)
                    {
                        var questions = context.questionmasters.ToList().OrderByDescending(q => q.QuestionId);
                        foreach (var q in questions)
                        {
                            var question = context.questionmasters.FirstOrDefault(s => s.id == q.id && s.QuestionId == q.QuestionId);
                            q.QuestionId++;
                            question.QuestionId = q.QuestionId;
                            context.SaveChanges();
                        }
                    }
                    var arrange = context.questionmasters.Where(q => q.id == questionid).FirstOrDefault();
                    arrange.QuestionId = positionid;
                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        var resultdata = context.questionmasters.ToList().OrderBy(q => q.id).Select(a => new QuestionModel()
                        {
                            id = a.id,
                            QuestionId = (int)a.QuestionId,
                            Question = a.Question,
                            IsActive = a.IsActive,
                            StoreId = (int)a.StoreId
                        });
                        response = resultdata;
                    }
                }
            }
            return response;
        }
    }
}
