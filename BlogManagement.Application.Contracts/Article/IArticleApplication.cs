using BlogManagement.Application.Contracts.Article;
using Framework.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        EditArticle EditGet(int id);
        OperationResult Edit(EditArticle command);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
