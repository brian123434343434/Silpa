USE
SILAMC_MAVDT
GO
/**************************************************************************************
 * Autor: Luis Cordoba Hoyos														  *
 * Fecha: Lunes, 30 de Noviembre de 2009											  *
 * Decripción: En este script se generan los cambios realizados en la tabla			  *
 * SECTOR donde se crean dos campos que permiten definir si el el tipo de proyecto es *
 * requiere DAA o TDR y a quien le corresponde la realizacion de este				  *	
 **************************************************************************************/
ALTER TABLE SECTOR ADD SEC_REQUIERE_DAA BIT NULL DEFAULT 1
GO

ALTER TABLE SECTOR ADD SEC_CORRESPONDE_MAVDT BIT NULL DEFAULT 1
GO