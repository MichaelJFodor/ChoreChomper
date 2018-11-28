<?php
	header("Access-Control-Allow-Origin: *");
	header("Content-Type: application/json; charset=UTF-8");
	include 'database.php';
	include 'User_Login.php';
	$username_attempt_ = $_GET["Username_Attempt"];
	$password_attempt_ = $_GET["Password_Attempt"];
	$database = new Database();
	$db = $database->getConnection();

	$user_login = new User_Login($db)

	$stmt = $user_login->login_read();

	if($stmt->rowCount() == 1)
	{
		$user_login_arr=array();
		$user_login_arr["records"]=array();

		while($row = $stmt->fetch(PDO::FETCH_ASSOC)){
			extract($row);
			$user_login_item=array("uid" => $uid, "username"=>$username, "password"=>$password);
		
		array_push($user_login_arr["records"], $user_login_item);
		}
		http_response_code(200);
 
    	// show products data in json format
    	echo json_encode($user_login_item);
	}
 else{
 
    // set response code - 404 Not found
    http_response_code(404);
 
    // tell the user no products found
    echo json_encode(
        array("message" => "Incorrect user");
    );
	}
?>