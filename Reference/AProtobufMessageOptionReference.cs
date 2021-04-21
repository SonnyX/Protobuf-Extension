
using System.Collections.Generic;

namespace ALittle
{
    public class AProtobufMessageOptionReference : AProtobufReferenceTemplate<AProtobufMessageOptionElement>
    {
        public AProtobufMessageOptionReference(ABnfElement element) : base(element) { }

        public override string QueryQuickInfo()
        {
            if (GeneralOptions.Instance.ProjectTeam == ProjectTeamTypes.LW)
            {
                var custom_type = m_element.GetCustomType();
                if (custom_type == null) return null;

                var id_list = custom_type.GetIdList();
                if (id_list.Count == 0) return null;

                var id = id_list[id_list.Count - 1];
                if (id_list.Count != 1 && id_list.Count != 2) return null;
                if (id_list.Count == 2 && id_list[0].GetElementText() != "ProtoDB") return null;

                var const_value = m_element.GetConst();
                if (const_value == null) return null;

                var const_text = const_value.GetText();
                var id_text = id.GetElementText();

                if (id_text == "primary")
                {
                    return "Fill in one field as the primary key or multiple fields as conforming to the primary key. Use multiple fields and separate them";
                }
                else if (id_text == "unique" || id_text == "index")
                {
                    return "Fill in one field as an index or multiple fields as conforming to the index ，Use multiple fields and separate them. Multiple groups can be defined, and multiple groups are separated by | ";
                }
                else if (id_text == "split_count")
                {
                    return "Define the number of sub-tables";
                }
                else if (id_text == "split_incr_interval")
                {
                    return "Define the interval interval of self-increasing ID between sub-tables";
                }
                else if (id_text == "split_incr_start")
                {
                    return "Define the starting value of the self-increasing ID of the first sub-table";
                }
                else if (id_text == "primary_incr")
                {
                    return "Define the primary key is self-increment, please fill in \"true\" or \"false\"";
                }
            }

            return null;
        }

        public override ABnfGuessError CheckError()
        {
            if (GeneralOptions.Instance.ProjectTeam == ProjectTeamTypes.LW)
            {
                var message_body = m_element.GetParent() as AProtobufMessageBodyElement;
                if (message_body == null) return null;

                var var_list = message_body.GetMessageVarList();
                var name_set = new HashSet<string>();
                foreach (var var_dec in var_list)
                {
                    var var_name = var_dec.GetMessageVarName();
                    if (var_name != null && !name_set.Contains(var_name.GetElementText()))
                        name_set.Add(var_name.GetElementText());
                }

                var custom_type = m_element.GetCustomType();
                if (custom_type == null) return null;

                var id_list = custom_type.GetIdList();
                if (id_list.Count == 0) return null;

                var id = id_list[id_list.Count - 1];
                if (id_list.Count != 1 && id_list.Count != 2) return null;
                if (id_list.Count == 2 && id_list[0].GetElementText() != "ProtoDB") return null;

                var const_value = m_element.GetConst();
                if (const_value == null) return null;

                var const_text = const_value.GetText();
                var id_text = id.GetElementText();

                if (id_text == "primary")
                {
                    if (const_text == null)
                        return new ABnfGuessError(const_value, id_text + "String assignment must be used");
                    var const_string = const_text.GetElementString();
                    if (const_string == "")
                        return new ABnfGuessError(const_value, id_text + "Cannot be an empty string");
                    var const_split = const_string.Split(',');
                    foreach (var const_var in const_split)
                    {
                        var const_var_trim = const_var.Trim();
                        if (const_var_trim.Length == 0)
                            return new ABnfGuessError(const_value, id_text +"Contains empty field names inside");

                        if (!name_set.Contains(const_var_trim))
                            return new ABnfGuessError(const_value, const_var_trim + "Not a field name");
                    }
                }
                else if (id_text == "unique" || id_text == "index")
                {
                    if (const_text == null)
                        return new ABnfGuessError(const_value, id_text + "String assignment must be used");
                    var const_string = const_text.GetElementString();
                    if (const_string == "")
                        return new ABnfGuessError(const_value, id_text + "Cannot be an empty string");
                    var const_combine_split = const_string.Split('|');
                    foreach (var const_combine in const_combine_split)
                    {
                        var const_split = const_combine.Split(',');
                        foreach (var const_var in const_split)
                        {
                            var const_var_trim = const_var.Trim();
                            if (const_var_trim.Length == 0)
                                return new ABnfGuessError(const_value, id_text + "Contains empty field names inside");

                            if (!name_set.Contains(const_var_trim))
                                return new ABnfGuessError(const_value, const_var_trim + "Not a field name");
                        }
                    }
                }
                else if (id_text == "split_count")
                {
                    if (const_text == null)
                        return new ABnfGuessError(const_value, id_text + "String assignment must be used ");
                    var const_string = const_text.GetElementString();

                    if (!int.TryParse(const_string, out int result))
                        return new ABnfGuessError(const_value, id_text + "Must be a number");
                    if (result < 2)
                        return new ABnfGuessError(const_value, id_text + "Must be greater than or equal to 2");
                }
                else if (id_text == "split_incr_interval")
                {
                    if (const_text == null)
                        return new ABnfGuessError(const_value, id_text + "String assignment must be used ");
                    var const_string = const_text.GetElementString();

                    if (!long.TryParse(const_string, out long result))
                        return new ABnfGuessError(const_value, id_text + "Must be a number ");
                    if (result <= 0)
                        return new ABnfGuessError(const_value, id_text + "Must be greater than 0 ");
                }
                else if (id_text == "split_incr_start")
                {
                    if (const_text == null)
                        return new ABnfGuessError(const_value, id_text + "String assignment must be used ");
                    var const_string = const_text.GetElementString();

                    if (!int.TryParse(const_string, out int result))
                        return new ABnfGuessError(const_value, id_text + "Must be a number ");
                    if (result < 0)
                        return new ABnfGuessError(const_value, id_text + "Must be greater than or equal to 0 ");
                }
                else if (id_text == "primary_incr")
                {
                    if (const_text == null)
                        return new ABnfGuessError(const_value, id_text + "String assignment must be used ");
                    var const_string = const_text.GetElementString();

                    if (const_string != "true" && const_string != "false")
                        return new ABnfGuessError(const_value, id_text + "Only fill in \"true\" or \"false\" ");
                }
            }

            return null;
        }
    }
}
