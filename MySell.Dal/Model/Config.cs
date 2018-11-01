using MySell.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySell.Dal.Model
{
    /// <summary>
    /// 配置
    /// </summary>
    public class Config
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public DateTime ActivityDate { get; set; }

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        public static Config Get()
        {
            return new ConfigRepository().Get();
        }

        public Feedback Update(Config config)
        {
            var configRepository = new ConfigRepository();
            var model = configRepository.Get();
            if (model == null)
                configRepository.Install(config);

            config.Id = model.Id;
            configRepository.Update(config);

            return Feedback.Ok("配置修改成功！");
        }
    }
}
