<?php 
	//this files inserts new chores into the database
	include 'connection.php';
	$conn = OpenCon();

	$ChoreName_ = $_GET["ChoreName"];
	$CompleteBy_ = $_GET["CompleteBy"];
	$AssignedTo_ = $_GET["AssignedTo"];
	$Priority_ = $_GET["Priority"];
	
	//makes sure that the person who it is assigned to actually exists
	$check = "SELECT user_id FROM chorechomper.user WHERE user_id = '$AssignedTo_'";
	$u_result = mysqli_query($conn,$check);
	$row = mysqli_fetch_assoc($u_result);
	$uid = $row['user_id'];
	
	if($u_result->num_rows != 0)
	{
		//inserts the new chore into the db
		$insertchore_sql = "INSERT into chores (chore_title, complete_by_date, assigned_to, priority) Values ('$ChoreName_', '$CompleteBy_', '$AssignedTo_', '$Priority_')";
		if(mysqli_query($conn,$insertchore_sql))
		{
			//obtains the chore_id and uses user id so that we can add it to the user has chores db
			$get_chore_id = "SELECT c_ID FROM chorechomper.chores WHERE chore_title = '$ChoreName_' AND complete_by_date = '$CompleteBy_' AND assigned_to = '$AssignedTo_' AND priority = '$Priority_'";
			$c_result = mysqli_query($conn,$get_chore_id);
			$row_c = mysqli_fetch_assoc($c_result);
			$cid = $row_c['c_ID'];
			$enter_into_relational = "INSERT INTO chorechomper.user_has_chores (user_id, chore_id) VALUES ('$uid', '$cid')";
			mysqli_query($conn,$enter_into_relational);
		}
	}
	CloseCon($conn);
?>
