<?php  //returns Chore
// required headers
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: access");
header("Access-Control-Allow-Methods: GET");
header("Access-Control-Allow-Credentials: true");
header("Content-Type: application/json; charset=UTF-8");
 
// include database and object files
include_once '../config/database.php';
include_once '../objects/chore.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// prepare chore object
$chore = new Chore($db);
 
// set ID property of record to read
$chore->c_ID = isset($_GET['c_ID']) ? $_GET['c_ID'] : die();
 
// read the details of chore to be edited
$chore->readOne();
if($chore->chore_title!=null){
    // create array
    $chore_arr = array(
        "choreId" =>  $chore->c_ID,
        "choreName" => $chore->chore_title,
        "deadlineTimestamp" => $chore->complete_by_date,
		"completedUID" => $chore->completed_by,
		"completedTimestamp" => $chore->completed_date,
		"priority" => $chore->priority,
		"assignedUserId" => $chore->assigned_to
 
    );
    
    // set response code - 200 OK
    http_response_code(201);

    // make it json format
    echo json_encode($chore_arr);
}
 
else{
    // set response code - 404 Not found
    http_response_code(404);
 
    // tell the user that chore does not exist
    echo json_encode(array("message" => "Chore does not exist."));
}

?>