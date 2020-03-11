/*
 * define.h
 *
 *  Created on: Feb 26, 2019
 *      Author: Yifat
 */

#ifndef GLOBAL_DEFINE_H_
#define GLOBAL_DEFINE_H_

/// Configuration data for EzRadio plugin manager.
#define EZRADIODRV_INIT_DEFAULT                                                 \
  {                                                                             \
    EZRADIODRV_TRANSMIT_PLUGIN_INIT_DEFAULT      /* Tx plugin init */           \
    EZRADIODRV_RECEIVE_PLUGIN_INIT_DEFAULT       /* Rx plugin init */           \
    EZRADIODRV_CRC_ERROR_PLUGIN_INIT_DEFAULT     /* CRC error plugin init */    \
    EZRADIODRV_AUTO_ACK_PLUGIN_INIT_DEFAULT      /* Auto-ack plugin init */     \
    EZRADIODRV_UNMOD_CARRIER_PLUGIN_INIT_DEFAULT /* CW plugin init */           \
    EZRADIODRV_PN9_PLUGIN_INIT_DEFAULT           /* PN9 plugin init */          \
    EZRADIODRV_DIRECT_TRANSMIT_PLUGIN_INIT_DEFAULT /* Direct Tx plugin init */  \
      EZRADIODRV_DIRECT_RECEIVE_PLUGIN_INIT_DEFAULT /* Direct Rx plugin init */ \
  }
//typedef enum _ConfigStage
//{
//	CONFIG_STAGE_1,
//	CONFIG_STAGE_2,
//}ConfigStage;

typedef enum _Headers
{
	//HEADER_MSR_FIRST = 		0xB0,
	HEADER_MSR = 			0xB1,
	HEADER_MSR_ACK = 		0xB2,
	HEADER_HST = 			0xB3,
	HEADER_HST_ACK = 		0xB4,
	HEADER_MSR_ACK_GET_PRM = 	0xB5,
	HEADER_SEN_PRM = 		0xB6,
	HEADER_SEN_PRM_ACK = 	0xB7,
	HEADER_MSR_ONLY	=		0xB8,
	HEADER_MSR_ONLY_ACK	=	0xB9,
	HEADER_MON_GETID = 		0xA1,
	HEADER_MON_GETID_ACK = 	0xA2,
	HEADER_MON_MSR = 		0xA3,
	HEADER_MON_MSR_ACK = 	0xA4,
	HEADER_MON_ID_OK = 		0xA5,
	HEADER_GETID = 			0xA6,
	HEADER_GETID_ACK = 		0xA7,
	HEADER_ID_OK = 			0xA8,
	HEADER_SND_DATA = 		0xC1,
	HEADER_SND_DATA_ACK = 	0xC2,
	HEADER_HUB_PRM =	 	0xC3,
	HEADER_HUB_PRM_ACK = 	0xC4,
	HEADER_HUB_SNS_PRM =	 0xC5,
	HEADER_HUB_SNS_PRM_ACK = 0xC6,
	HEADER_TEST_RF	=		0xD1,
	HEADER_TEST_RF_ACK	= 	0xD2,
}Headers;


typedef enum _MsgType
{
	TYPE_DATA,
	TYPE_PRM,
	TYPE_DATA_NEW,
//	TYPE_HSTR,
}MsgType;

typedef union _Uint16toBytes
{
	uint16_t iVal;
    uint8_t bVal[2];
}Uint16toBytes;

typedef union _Int16toBytes
{
	int16_t iVal;
    uint8_t bVal[2];
}Int16toBytes;

typedef union _Uint32toBytes
{
	uint32_t iVal;
    uint8_t bVal[4];
}Uint32toBytes;

union Int32toBytes
{
	int32_t iVal;
    uint8_t bVal[4];
} ;

union FloatToBytes
{
	float fVal;
    uint8_t bVal[4];
} ;

#define DEFAULT_ID	0xFFFFFFFF

#define TYPE_NONE	0
#define TYPE_PIVOT	67
#define TYPE_TENS	71
#define TYPE_SMS	72
#define TYPE_SD		73
#define TYPE_DER	74
#define TYPE_FI_3	83
#define TYPE_AIR_TMP_ALRTS	84
#define TYPE_IRRIGATION		85

#define FIRST_FIELD		0
#define FIRST_FIELD_LEN	1

#endif /* GLOBAL_DEFINE_H_ */
