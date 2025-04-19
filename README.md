# **AI-SQL: AI-Powered SQL Query Converter**

AI-SQL is a tool that converts natural language commands into SQL queries using artificial intelligence. This project allows users to interact with databases without needing to write SQL themselves. AI models analyze the natural language inputs and generate the correct SQL queries.

---

## **Features**

- **Natural Language to SQL Conversion:** Users can create SQL queries from natural language input.
- **AI-Powered Solution:** AI models like GPT-3 are used to generate accurate and optimized SQL queries.
- **Database Support:** Supports popular databases like SQL Server, MySQL, and PostgreSQL.
- **Complex Queries:** Supports complex SQL commands such as `JOIN`, `GROUP BY`, `WHERE`, etc.

---

## **How It Works**

1. **User Input:** The user provides a natural language SQL command (e.g., "List the salaries of all employees").
2. **AI Conversion:** The AI analyzes the input and converts it into an appropriate SQL query.
3. **Query Execution:** The generated SQL query is executed on the database, and the results are returned to the user.

---

## **Getting Started**

To run this project locally, follow these steps:

### 1. **Clone the Project**

```bash
git clone https://github.com/umutekici/AISqlApi.git
cd AISqlApi

### 2. **Set Up Your API Key**

You will need an API key to use the AI model (GPT-3 or similar). Add your API key to the **appsettings.json** file.

```json
{
  "OpenAI": {
    "ApiKey": "your-api-key-here"
  }
}

### 3. **Run the Project**

Use the following command to run the project:

```bash
dotnet run


