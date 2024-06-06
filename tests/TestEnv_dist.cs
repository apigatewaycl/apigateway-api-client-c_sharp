/*
 * API Gateway: Cliente de API en C#.
 * Copyright (C) API Gateway <https://www.apigateway.cl>
 *
 * Este programa es software libre: usted puede redistribuirlo y/o modificarlo
 * bajo los términos de la GNU Lesser General Public License (LGPL) publicada
 * por la Fundación para el Software Libre, ya sea la versión 3 de la Licencia,
 * o (a su elección) cualquier versión posterior de la misma.
 *
 * Este programa se distribuye con la esperanza de que sea útil, pero SIN
 * GARANTÍA ALGUNA; ni siquiera la garantía implícita MERCANTIL o de APTITUD
 * PARA UN PROPÓSITO DETERMINADO. Consulte los detalles de la GNU Lesser General
 * Public License (LGPL) para obtener una información más detallada.
 *
 * Debería haber recibido una copia de la GNU Lesser General Public License
 * (LGPL) junto a este programa. En caso contrario, consulte
 * <http://www.gnu.org/licenses/lgpl.html>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    class TestEnv_dist
    {
        public TestEnv_dist()
        {

        }

        public void SetVariablesDeEntorno()
        {
            // Variables de ambiente de API Gateway
            string APIGATEWAY_API_TOKEN = ""; // TOKEN API GATEWAY
            string APIGATEWAY_API_URL = "https://apigateway.cl"; // URL API GATEWAY

            // Variables de Contribuyente de primera categoría
            string USUARIO_RUT = ""; // RUT DE USUARIO SII
            string USUARIO_CLAVE = ""; // CLAVE DE USUARIO SII

            // Variables de BHE
            string TEST_BHE_FECHA = "202405"; // PERIODO DE BHE A CONSULTAR
            string TEST_BHE_CODIGO_PDF = ""; // Código de PDF BHE Emitida

            // Variables de Indicador UF
            string TEST_UF_FECHA = "2024-05-17"; // DÍA DE INDICADORES A CONSULTAR
            string TEST_UF_MES = "202405"; // PERIODO DE INDICADORES A CONSULTAR
            string TEST_UF_ANIO = "2024"; // AÑO DE INDICADORES A CONSULTAR
            float TEST_UF_VALOR = 37354.68f; // Valor UF en el día TEST UF_FECHA

            Environment.SetEnvironmentVariable("APIGATEWAY_API_TOKEN", APIGATEWAY_API_TOKEN);
            Environment.SetEnvironmentVariable("APIGATEWAY_API_URL", APIGATEWAY_API_URL);

            Environment.SetEnvironmentVariable("USUARIO_RUT", USUARIO_RUT);
            Environment.SetEnvironmentVariable("USUARIO_CLAVE", USUARIO_CLAVE);

            Environment.SetEnvironmentVariable("TEST_BHE_FECHA", TEST_BHE_FECHA);
            Environment.SetEnvironmentVariable("TEST_BHE_CODIGO_PDF", TEST_BHE_CODIGO_PDF);

            Environment.SetEnvironmentVariable("TEST_UF_FECHA", TEST_UF_FECHA);
            Environment.SetEnvironmentVariable("TEST_UF_MES", TEST_UF_MES);
            Environment.SetEnvironmentVariable("TEST_UF_ANIO", TEST_UF_ANIO);
            Environment.SetEnvironmentVariable("TEST_UF_VALOR", TEST_UF_VALOR.ToString());
        }
    }
}
