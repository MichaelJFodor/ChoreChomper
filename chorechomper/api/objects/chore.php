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
}