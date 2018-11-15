<?php
class Chore{
 
    // database connection and table name
    private $conn;
    private $table_name = "chores";
 
    // object properties
    public $c_id;
    public $c_name;
    public $c_date;
 
    // constructor with $db as database connection
    public function __construct($db){
        $this->conn = $db;
    }

    // read products
	function read(){
 
    // select all query
    $query = "SELECT
                p.c_id, p.c_name, p.c_date
            FROM
                " . $this->table_name . " p";
 
    // prepare query statement
    $stmt = $this->conn->prepare($query);
 
    // execute query
    $stmt->execute();
 
    return $stmt;
	}

    // used when filling up the update chore form
    function readOne(){
     
        // query to read single record
        $query =  "SELECT
                p.c_id, p.c_name, p.c_date
            FROM
                " . $this->table_name . " p 
            WHERE
                p.c_id = ?
            LIMIT
                0,1";
     
        // prepare query statement
        $stmt = $this->conn->prepare( $query );
     
        // bind id of product to be updated
        $stmt->bindParam(1, $this->c_id);  
        // execute query
        $stmt->execute();
     
        // get retrieved row
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
     
        // set values to object properties
        $this->c_id = $row['c_id'];
        $this->c_name = $row['c_name'];
        $this->c_date = $row['c_date'];
        return;
    }

}
?>