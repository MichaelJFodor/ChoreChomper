<?php
//all print statements are for browser testing. will not show up in the app
//openiing function
function OpenCon()
 {
 	//connection credentials to kyle's database
 	$servername = "localhost";
 	//$servername = "98.207.235.91";
	$username = "chorechomper";
	$password = "chorechomper";
	$database = "chorechomper";

// Create connection
	$conn = mysqli_connect($servername, $username, $password,$database);

// Check connection
	if (!$conn) {
   	 die("Connection failed: " . mysqli_connect_error());
	}
	//echo "Connected successfullyff";

 
 	return $conn;
 }
 //close connection 
function CloseCon($conn)
 {
 $conn -> close();
 }
   
?>

