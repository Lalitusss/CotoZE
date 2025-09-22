import React, { useState, useEffect } from "react";
import { Button, Modal, Form, Row, Col, Table } from "react-bootstrap";

const apiBaseUrl = "https://localhost:7029/api";

export default function ReservaApp() {
  const [reservas, setReservas] = useState([]);
  const [showModal, setShowModal] = useState(false);

  const [salonId, setSalonId] = useState(1);
  const [nombreCliente, setNombreCliente] = useState("");
  const [fecha, setFecha] = useState("");
  const [horaInicio, setHoraInicio] = useState("");
  const [horaFin, setHoraFin] = useState("");
  const nombresSalones = {
    1: "Salón Principal",
    2: "Salón Festejo",
    3: "Salón Fiesta",
  };

  useEffect(() => {
    const fetchReservas = async () => {
      try {
        const res = await fetch(`${apiBaseUrl}/reserva`);
        if (!res.ok) throw new Error(`Error HTTP: ${res.status}`);
        const data = await res.json();
        setReservas(data);
      } catch (error) {
        console.error("Fetch error:", error);
        // Aquí puedes setear estado de error para mostrar alerta o fallback
      }
    };

    fetchReservas();
  }, []);

  const crearReserva = async () => {
    const nuevaReserva = {
      salonId: parseInt(salonId),
      nombreCliente,
      fecha,
      horaInicio,
      horaFin,
    };
    console.log(JSON.stringify(nuevaReserva));
    try {
      const response = await fetch(`${apiBaseUrl}/reserva`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(nuevaReserva),
      });

      if (response.ok) {
        const reservaCreada = await response.json();
        setReservas([...reservas, reservaCreada]);
        setNombreCliente("");
        setFecha("");
        setHoraInicio("");
        setHoraFin("");
        setShowModal(false);
      } else {
        // Leer el cuerpo de error enviado por el backend
        const errorResponse = await response.json();
        alert(errorResponse.message || JSON.stringify(errorResponse));
      }
    } catch (error) {
      // Captura errores de red o excepciones inesperadas
      alert(error.message);
    }
  };

  return (
    <div className="container mt-4">
      <h2>Reservas</h2>{" "}
      <Button
        variant="primary"
        onClick={() => setShowModal(true)}
        className="mb-3"
      >
        Nueva Reserva
      </Button>
      <Table striped bordered hover responsive>
        <thead>
          <tr>
            <th>Cliente</th>
            <th>Fecha</th>
            <th>Hora Inicio</th>
            <th>Hora Fin</th>
            <th>Salón</th>
          </tr>
        </thead>
        <tbody>
          {reservas.map((r) => (
            <tr key={r.id}>
              <td>{r.nombreCliente}</td>
              <td>
                {new Date(r.fecha).toLocaleDateString("es-ES", {
                  day: "2-digit",
                  month: "2-digit",
                  year: "numeric",
                })}
              </td>
              <td>{r.horaInicio}</td>
              <td>{r.horaFin}</td>
              <td>{nombresSalones[r.salonId] || "Desconocido"}</td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Crear Nueva Reserva</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3" controlId="formSalon">
              <Form.Label>Salón</Form.Label>
              <Form.Select
                value={salonId}
                onChange={(e) => setSalonId(e.target.value)}
              >
                {Object.entries(nombresSalones).map(([id, nombre]) => (
                  <option key={id} value={id}>
                    {nombre}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>

            <Form.Group className="mb-3" controlId="formNombreCliente">
              <Form.Label>Nombre Cliente</Form.Label>
              <Form.Control
                type="text"
                value={nombreCliente}
                onChange={(e) => setNombreCliente(e.target.value)}
              />
            </Form.Group>

            <Row>
              <Col>
                <Form.Group className="mb-3" controlId="formFecha">
                  <Form.Label>Fecha</Form.Label>
                  <Form.Control
                    type="date"
                    value={fecha}
                    onChange={(e) => setFecha(e.target.value)}
                  />
                </Form.Group>
              </Col>
              <Col>
                <Form.Group className="mb-3" controlId="formHoraInicio">
                  <Form.Label>Hora Inicio</Form.Label>
                  <Form.Control
                    type="time"
                    value={horaInicio}
                    onChange={(e) => setHoraInicio(e.target.value)}
                  />
                </Form.Group>
              </Col>
              <Col>
                <Form.Group className="mb-3" controlId="formHoraFin">
                  <Form.Label>Hora Fin</Form.Label>
                  <Form.Control
                    type="time"
                    value={horaFin}
                    onChange={(e) => setHoraFin(e.target.value)}
                  />
                </Form.Group>
              </Col>
            </Row>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Cancelar
          </Button>
          <Button variant="primary" onClick={crearReserva}>
            Guardar Reserva
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}
