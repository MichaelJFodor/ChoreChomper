<?php
class Chore{
 
    // database connection and table name
    private $conn;
    private $table_name = "chores";
 
    // object properties
    public $c_ID;
	public $u_ID;
	public $g_ID;
    public $chore_title;
    public $complete_by_date;
	public $completed_by;
    public $completed_date;
    public $priority;
	public $assigned_to;
	
 
    // constructor with $db as database connection
    public function __construct($db){
        $this->conn = $db;
    }

    // read products
	function read(){
 
    // select all query
    $query = "SELECT
                p.c_ID, p.chore_title, p.complete_by_date, p.completed_by, p.completed_date, p.priority, p.assigned_to
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
                p.c_ID, p.chore_title, p.complete_by_date, p.completed_by, p.completed_date, p.priority, p.assigned_to
            FROM
                " . $this->table_name . " p 
            WHERE
                p.c_ID = ?
            LIMIT
                0,1";
     
        // prepare query statement
        $stmt = $this->conn->prepare( $query );
     
        // bind id of product to be updated
        $stmt->bindParam(1, $this->c_ID);  
        // execute query
        $stmt->execute();
     
        // get retrieved row
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
     
        // set values to object properties
        $this->c_ID = $row['c_ID'];
        $this->chore_title = $row['chore_title'];
        $this->complete_by_date = $row['complete_by_date'];
		$this->completed_by = $row['completed_by'];
        $this->completed_date = $row['completed_date'];
		$this->priority = $row['priority'];
		$this->assigned_to = $row['assigned_to'];
        return;
    }
	// used to fill user's chores
	function readUserChores(){
     
        // query to read 
        $query =  "SELECT 
				p.c_ID, p.chore_title, p.complete_by_date, p.completed_by, p.completed_date, p.priority, p.assigned_to
			FROM 
				chorechomper.chores p 
			WHERE 
				p.c_ID 
			IN 
				(SELECT 
					d.chore_id
				FROM
					chorechomper.user_has_chores d
				WHERE 
					d.user_id = ?)";
     
        // prepare query statement
    $stmt = $this->conn->prepare($query);
	
	// bind id of product to be updated
        $stmt->bindParam(1, $this->u_ID); 
 
    // execute query
    $stmt->execute();
 
    return $stmt;
    }
	//gets group's chores
	function readGroupChores(){
     
        // query to read groups
        $query =  "SELECT 
				p.c_ID, p.chore_title, p.complete_by_date, p.completed_by, p.completed_date, p.priority, p.assigned_to
			FROM 
				chorechomper.chores p
			WHERE 
				p.c_ID
			IN
				(SELECT 
					d.chore_id 
				FROM 
					chorechomper.user_has_chores d 
				WHERE 
					d.user_id 
				IN
					(SELECT 
						b.user_id 
					FROM 
						chorechomper.group_has_users b
					WHERE 
						b.id_group = ?))";
     
        // prepare query statement
    $stmt = $this->conn->prepare($query);
	
	// bind id of product to be updated
        $stmt->bindParam(1, $this->g_ID); 
 
    // execute query
    $stmt->execute();
 
    return $stmt;
    }
	//gets list of groups that a user is a member of
	function readGroups(){
     
        // query to read 
        $query =  "SELECT 
					p.idGroup, p.nameGroup 
				FROM 
					chorechomper.group p
				WHERE 
					p.idGroup 
				IN
					(SELECT 
						d.id_group
					FROM 
						chorechomper.group_has_users d
					WHERE 
						d.user_ID = ?)";
     
        // prepare query statement
    $stmt = $this->conn->prepare($query);
	
	// bind id of product to be updated
        $stmt->bindParam(1, $this->u_ID); 
 
    // execute query
    $stmt->execute();
 
    return $stmt;
    }

}
?>