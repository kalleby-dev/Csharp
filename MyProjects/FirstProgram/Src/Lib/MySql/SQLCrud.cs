using System;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace FirstProgram.Src.Lib.MySql
{
    #nullable enable
    public abstract class SQLCrud : SQLConnector
    {
        
        
        protected String? statement = null;
        protected Dictionary <string, Object>? param = null;
        protected Dictionary <String, Object> data = new Dictionary <string, Object> ();

        public Dictionary <String, Object> Data { 
            get{return this.data;}
        }

        
        ///<summary>Insere um novo registro no banco de dados retorna o ID do registro</summary>
        protected long insert(String table, Dictionary <String, Object> data, bool timestamp = false){
            this.open();
            var stmt = this.Connection.CreateCommand();

            // Cria o cmd de Inserção dinamicamente
            var sqlColumns = String.Join(",", data.Keys);
            var sqlParam = String.Join(",",  (data.Keys).Select( value => String.Format($"?{value}")) );
            stmt.CommandText = $"INSERT INTO {table}({sqlColumns}) VALUES ({sqlParam})";

            // Prepara a statement com os valores
            foreach (var item in data){
                stmt.Parameters.AddWithValue($"?{item.Key}", $"{item.Value}");
            }
            
            try{
                // Executa a insersão e retorna o ID do registro
                stmt.ExecuteNonQuery();
                return stmt.LastInsertedId;
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}");
                throw new NullReferenceException("Não foi possivel efetuar o registro");
            }
            finally{
                this.close();
            }
        }
        

        // Realiza uma busca no banco de dados
        protected List<Dictionary<String, Object>> read(){
            this.open();
            var stmt = this.Connection.CreateCommand();

            // Prepara o comando SQL
            stmt.CommandText = $"{this.statement}";
            
            // Prepara a statement com os valores
            if(this.param != null){
                foreach (var item in this.param){
                    stmt.Parameters.AddWithValue(item.Key, item.Value);  
                } 
            }

            try{
                // Realiza a busca e retorna os resultados
                var rows = stmt.ExecuteReader(); 
            
                var list = new List<Dictionary <String, Object>> ();
                
                while (rows.Read()){
                    var tempData = new Dictionary <String, Object>();

                    for (int i = 0; i < rows.FieldCount; i++){
                        tempData[ rows.GetName(i) ] = rows.GetValue(i);
                    }

                    list.Add(tempData); 
                }
                return list;
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}");
                throw new NullReferenceException("Não foi possivel efetuar a busca");
            }
            finally{
                this.close();
            }  
        }

        

        protected void update( Array data){

        }
        protected void delete( Array data){

        }
        

        
    }
    #nullable disable
}
