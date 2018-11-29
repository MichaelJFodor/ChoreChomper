<?php
//this files has functions that will allow other files to connect to the database
function OpenCon()
 {
 	//connection credentials to kyle's database
 	$servername = "localhost";
 	//$servername = "98.207.235.91";
	$username = "root";
	$password = "";
	$database = "chorechomper";

// Create connection
	$conn = mysqli_connect($servername, $username, $password,$database);

// Check connection
	if (!$conn) {
   	 die("Connection failed: " . mysqli_connect_error());
	}
 	return $conn;
 }
 //close connection 
function CloseCon($conn)
 {
 $conn -> close();
 }
   
?>

