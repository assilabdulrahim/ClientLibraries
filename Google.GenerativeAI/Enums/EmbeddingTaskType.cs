namespace Google.GenerativeAI.Enums
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum EmbeddingTaskType
    {
        [EnumMember(Value = "SEMANTIC_SIMILARITY")]
        [Description("Embeddings optimized to assess text similarity.")]
        SemanticSimilarity,

        [EnumMember(Value = "CLASSIFICATION")]
        [Description("Embeddings optimized to classify texts according to preset labels.")]
        Classification,

        [EnumMember(Value = "CLUSTERING")]
        [Description("Embeddings optimized to cluster texts based on their similarities.")]
        Clustering,

        [EnumMember(Value = "RETRIEVAL_DOCUMENT")]
        [Description("Embeddings optimized for document search.")]
        RetrievalDocument,

        [EnumMember(Value = "RETRIEVAL_QUERY")]
        [Description("Embeddings optimized for general search queries. Use RETRIEVAL_QUERY for queries; RETRIEVAL_DOCUMENT for documents to be retrieved.")]
        RetrievalQuery,

        [EnumMember(Value = "CODE_RETRIEVAL_QUERY")]
        [Description("Embeddings optimized for retrieval of code blocks based on natural language queries. Use CODE_RETRIEVAL_QUERY for queries; RETRIEVAL_DOCUMENT for code blocks to be retrieved.")]
        CodeRetrievalQuery,

        [EnumMember(Value = "QUESTION_ANSWERING")]
        [Description("Embeddings for questions in a question-answering system, optimized for finding documents that answer the question. Use QUESTION_ANSWERING for questions; RETRIEVAL_DOCUMENT for documents to be retrieved.")]
        QuestionAnswering,

        [EnumMember(Value = "FACT_VERIFICATION")]
        [Description("Embeddings for statements that need to be verified, optimized for retrieving documents that contain evidence supporting or refuting the statement. Use FACT_VERIFICATION for the target text; RETRIEVAL_DOCUMENT for documents to be retrieved.")]
        FactVerification
    }
}