<?php

/**
 * Created by PhpStorm.
 * User: mehaX
 * Date: 9/11/2016
 * Time: 3:04 PM
 */
class Home extends CI_Controller
{
    public function __construct()
    {
        parent::__construct();
        $this->load->helper('url');

        $this->load->model('sudoku_model');
    }

    public function index()
    {
        $data['sudokus'] = $this->sudoku_model->get_sudokus();

        $this->load->view('header');
        $this->load->view('index', $data);
        $this->load->view('footer');
    }

    public function get_sudoku()
    {
        $sudoku_id          = $this->input->post('sudoku_id');
        $sudoku             = $this->sudoku_model->get_sudoku($sudoku_id);
        $sudoku['value']    = unserialize($sudoku['value']);

        echo json_encode($sudoku);
    }

    public function insert()
    {
        $sudoku = array(
            'date'  => time(),
            'value' => serialize($this->input->post('value'))
        );

        $id = $this->sudoku_model->insert($sudoku);
        echo json_encode(array('id' => $id, 'date' => date('d/m/Y H:i:s', $sudoku['date'])));
    }
}