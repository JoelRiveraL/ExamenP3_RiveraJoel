Feature: EditarBien

A short summary of the feature

@tag1
Scenario: Editar Pedido Existente Bien
	Given Un pedido existente en la base de datos .
	| PedidoID | ClienteID | Monto | Estado   |
	| 1        | 2         | 150   | Aprobado |
	When Edito el pedido con nuevos datos correctos .
	| PedidoID | ClienteID | Monto | Estado   |
	| 1        | 2         | 100   | Aprobado |
	Then El pedido debe actualizarse en la base de datos.
	| PedidoID | ClienteID | Monto | Estado   |
	| 1        | 2         | 100   | Aprobado |
