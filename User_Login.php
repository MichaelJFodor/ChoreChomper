<?php 
class User_Login{
	private $conn;
	private $table_name = "chores";

	public $uid;
	public $username;
	public $password;
	//public $login_value;

	public function __construct($db){
		$this->conn = $db;
	}

	public User_login($id,$user,$pass)
	{
		$uid = $id;
		$username = $user;
		$password = $pass;
	}

	function read(){
		$query = "SELECT user_id FROM user WHERE Password = '$password' AND username = '$username'";
		
		$stmt = $this->conn->prepare($query);

		$stmt->execute();

		return $stmt;
	}


}
?>