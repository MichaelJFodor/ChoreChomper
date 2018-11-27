<?php 
	include 'connection.php';
	$conn = OpenCon();
	$ChoreName_ = $_GET["ChoreName"];
	$CompleteBy_ = $_GET["CompleteBy"];
	$AssignedTo_ = $_GET["AssignedTo"];
	$Priority_ = $_GET["Priority"];
	$check = "SELECT user_id FROM chorechomper.user WHERE Username = '$AssignedTo_'";
	$u_result = mysqli_query($conn,$check);
	$row = mysqli_fetch_assoc($u_result);
	$uid = $row['user_id'];
	if($u_result->num_rows != 0)
	{
		$sql = "INSERT into chores (chore_title, complete_by_date, assigned_to, priority) Values ('$ChoreName_', '$CompleteBy_', '$AssignedTo_', '$Priority_')";
		if(mysqli_query($conn,$sql))
		{
			echo 'good';
			$get_chore_id = "SELECT c_ID FROM chorechomper.chores WHERE chore_title = '$ChoreName_' AND complete_by_date = '$CompleteBy_' AND assigned_to = '$AssignedTo_' AND priority = '$Priority_'";
			$c_result = mysqli_query($conn,$get_chore_id);
			$row_c = mysqli_fetch_assoc($c_result);
			$cid = $row_c['chore_id'];
			$enter = "INSERT INTO chorechomper.user_has_chores (user_id, chore_id) VALUES ('$uid', '$cid')";
			if(mysqli_query($conn,$enter))
			{
				echo 'good';
			}
			else
				echo $uid . $cid;
		}
	}
	CloseCon($conn);
?>
