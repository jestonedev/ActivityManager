using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMClasses
{
    public sealed class PlugActionHelper
    {
        private PlugActionHelper()
        {
        }

        public static Collection<PlugActionParameter> ConvertActivityStepToPlugParameters(Collection<ActivityStepParameter> inputParameters,
            Collection<ActivityStepParameter> outputParameters)
        {
            if (inputParameters == null)
                throw new AMException("Не заданна ссылка на список входных параметров");
            if (outputParameters == null)
                throw new AMException("Не заданна ссылка на список выходных параметров");
            Collection<PlugActionParameter> parameters = new Collection<PlugActionParameter>();
            foreach (ActivityStepParameter action_parameter in inputParameters)
            {
                PlugActionParameter plug_parameter = new PlugActionParameter(action_parameter.Name, null, ParameterDirection.Input);
                parameters.Add(plug_parameter);
            }
            foreach (ActivityStepParameter action_parameter in outputParameters)
            {
                PlugActionParameter plug_parameter = new PlugActionParameter(action_parameter.Name, null, ParameterDirection.Output);
                parameters.Add(plug_parameter);
            }
            return parameters;
        }

        public static PlugActionInfo FindPlugAction(List<PlugInfo> plugins, ActivityStep step)
        {
            if (step == null)
                throw new AMException("Не заданна ссылка на шаг прохода ActivityStep");
            if (plugins == null)
                throw new AMException("Не заданна ссылка на список загруженных плагинов");
            Collection<PlugActionParameter> chk_parameters = PlugActionHelper.ConvertActivityStepToPlugParameters(step.InputParameters, step.OutputParameters);
            foreach (PlugInfo plugin in plugins)
                if (step.PlugName == plugin.PlugName)
                    foreach (PlugActionInfo action in plugin.PlugActions)
                        if (action.ActionName == step.ActionName)
                        {
                            if (action.Parameters.Count != chk_parameters.Count)
                                continue;
                            //Если имена методов и число параметров совпало, то проверяем входные параметры на соответствие имен и направлений
                            bool action_is_equal = true;
                            foreach (PlugActionParameter parameter in action.Parameters)
                            {
                                bool parameter_founded = false;
                                foreach (PlugActionParameter chk_parameter in chk_parameters)
                                {
                                    if ((chk_parameter.Name == parameter.Name) && (chk_parameter.Direction == parameter.Direction))
                                    {
                                        parameter_founded = true;
                                        break;
                                    }
                                }
                                if (!parameter_founded)
                                {
                                    action_is_equal = false;
                                    break;
                                }
                            }
                            if (action_is_equal)
                                return action;
                        }
            return null;
        }
    }
}
