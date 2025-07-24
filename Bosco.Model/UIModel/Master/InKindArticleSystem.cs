using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.UIModel
{
    public class InKindArticleSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public InKindArticleSystem()
        {

        }

        public InKindArticleSystem(int InKindArticleId)
        {
            FillInKindArticleProperties(InKindArticleId);
        }
        #endregion

        #region In Kind Article Properties
        public int ArticleId { get; set; }
        public string Abbrevation { get; set; }
        public string Article { get; set; }
        public double OpQuantity { get; set; }
        public double OpValue { get; set; }
        public string Notes { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchInKindArticleDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.InKindArticle.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteInKindArticleDetails(int InKindArticleId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.InKindArticle.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.InKindArticle.ARTICLE_IDColumn, InKindArticleId);
                resultArgs = dataMember.UpdateData(dataMember, "", SQLType.SQLStatic);
            }
            return resultArgs;
        }

        public ResultArgs SaveInKindArticleDetails()
        {
            using (DataManager dataManager = new DataManager((ArticleId == 0) ? SQLCommand.InKindArticle.Add : SQLCommand.InKindArticle.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.InKindArticle.ABBREVATIONColumn, Abbrevation);
                dataManager.Parameters.Add(this.AppSchema.InKindArticle.ARTICLEColumn, Article);
                dataManager.Parameters.Add(this.AppSchema.InKindArticle.OP_QUANTITYColumn, OpQuantity);
                dataManager.Parameters.Add(this.AppSchema.InKindArticle.OP_VALUEColumn, OpValue);
                dataManager.Parameters.Add(this.AppSchema.InKindArticle.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.InKindArticle.ARTICLE_IDColumn, ArticleId);
                resultArgs = dataManager.UpdateData(dataManager, "", SQLType.SQLStatic);
            }
            return resultArgs;
        }

        public void FillInKindArticleProperties(int InKindArticleId)
        {
            resultArgs = FetchInKindArticleById(InKindArticleId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Abbrevation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindArticle.ABBREVATIONColumn.ColumnName].ToString();
                Article = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindArticle.ARTICLEColumn.ColumnName].ToString();
                OpQuantity = this.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindArticle.OP_QUANTITYColumn.ColumnName].ToString());
                OpValue = this.NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindArticle.OP_VALUEColumn.ColumnName].ToString());
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindArticle.NOTESColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchInKindArticleById(int InKindArticleId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.InKindArticle.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.InKindArticle.ARTICLE_IDColumn, InKindArticleId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
